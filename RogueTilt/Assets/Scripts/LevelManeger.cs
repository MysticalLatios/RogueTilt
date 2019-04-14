using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManeger : MonoBehaviour
{
    // Vars for our Level
    private int columns = 5;
    private int rows = 4;
    public GameObject exitTile;
    private GameObject startTile;
    private Global global;
    public List<List<GameObject>> floorTiles = new List<List<GameObject>>(); //All the other tiles

    private Camera MainCamera;

    int offset = 60;

    //How far can start and end be?
    int miniDistance = 2;


    //Put the cords in the grid
    void InitialiseList()
    {
        //Make sure its empty
        floorTiles.Clear();

        //x axis, columns
        for (int x = 0; x < columns; x++)
        {
            floorTiles.Add(new List<GameObject>());
            // y axis, rows
            for (int y = 0; y < rows; y++)
            {
                if (Random.Range(1, 6) == 1)
                {
                    floorTiles[x].Add(null);
                    //Debug.Log("Position at: " + x + " " + y + "Not created");
                }

                else
                {
                    floorTiles[x].Add(Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/Basic Room")));
                    floorTiles[x][y].transform.position = new Vector3((x + 1) * offset, 0, (y + 1) * offset);
                   //Debug.Log("Position at: " + x + " " + y + "Created");
                }
            }
        }

        //Set start tile
        Vector3 start_pos = new Vector3(Random.Range(0, columns -1), Random.Range(0, rows-1), 0);
        startTile = Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/Start Room"));
        startTile.transform.position = new Vector3((start_pos.x + 1) * offset, 0, (start_pos.y + 1) * offset);
        floorTiles[(int)start_pos.x][(int)start_pos.y] = startTile;

        //Set end tile
        Vector3 end_pos = new Vector3(Random.Range(0, columns -1), Random.Range(0, rows -1), 0);
        //ToDo: Make sure they are farther apart

        bool farEnough = (checkDistance(start_pos, end_pos, miniDistance));

        int i = 0;
        int compromiseValue = miniDistance;
        while (end_pos == start_pos || !farEnough)
        {
            Debug.Log("Rerolling");
            
            end_pos = new Vector3(Random.Range(0, columns - 1), Random.Range(0, rows - 1), 0);
            //Give up tring to find a min distance after X number of tries
            if(i % 10 == 0)
            {
                farEnough = true;
            }
            else
            {
                farEnough = (checkDistance(start_pos, end_pos, 0));
            }
            i++;
        }
        exitTile = Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/End Room"));
        exitTile.transform.position = new Vector3((end_pos.x + 1) * offset, 0, (end_pos.y + 1) * offset);
        floorTiles[(int)end_pos.x][(int)end_pos.y] = exitTile;

    }

    //true if end is far enough away
    bool checkDistance(Vector3 start, Vector3 end, int howFar)
    {
        Vector3 upper_bound = new Vector3(start.x + miniDistance, start.y + howFar, 0);
        Vector3 lower_bound = new Vector3(start.x - miniDistance, start.y - howFar, 0);

        if((end.x > lower_bound.x && end.x < upper_bound.x) && (end.y > lower_bound.y && end.y < upper_bound.y))
        {
            return false;
        }

        return true;
    }


    // Start is called before the first frame update
    // Init the board
    void Start()
    {
        global = GameObject.Find("GlobalObject").GetComponent<Global>();

        //Set up the Tiles
        InitialiseList();

        //Check if dfs can find its way out of the loop, if so, run InitialiseList() again

        //Set the active Tile as the start tile
        global.SetActiveTile(startTile);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
