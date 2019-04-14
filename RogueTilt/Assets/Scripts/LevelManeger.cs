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

        bool notFarEnough = (isNotFarEnough(start_pos, end_pos, miniDistance));

        int i = 0;
        int compromiseDistance = miniDistance;
        while (end_pos == start_pos || notFarEnough)
        {
            i++;
            Debug.Log("Rerolling: " + (end_pos == start_pos) + " " + notFarEnough);
            
            end_pos = new Vector3(Random.Range(0, columns - 1), Random.Range(0, rows - 1), 0);
            //Give up tring to find a min distance after X number of tries
            if(i % 10 == 0)
            {
                compromiseDistance--;
                Debug.Log("Compromising...");
            }
            else
            {
                notFarEnough = (isNotFarEnough(start_pos, end_pos, compromiseDistance));
            }
            
        }
        Debug.Log((notFarEnough) + " " + end_pos.x + "," + end_pos.y + " " + start_pos.x + "," + start_pos.y);
        exitTile = Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/End Room"));
        exitTile.transform.position = new Vector3((end_pos.x + 1) * offset, 0, (end_pos.y + 1) * offset);
        floorTiles[(int)end_pos.x][(int)end_pos.y] = exitTile;

    }

    //true if end is not far enough away
    bool isNotFarEnough(Vector3 start, Vector3 end, int howFar)
    {
        Vector3 upper_bound = new Vector3(start.x + howFar, start.y + howFar, 0);
        Vector3 lower_bound = new Vector3(start.x - howFar, start.y - howFar, 0);

        if(((end.x >= lower_bound.x && end.x <= upper_bound.x) && (end.y >= lower_bound.y && end.y <= upper_bound.y)))
        {
            return true;
        }
        return false;
    }


    //find the end from the start
    int depthFirstSearch(Vector3 start_pos)
    {
        //Stack contains the Vector3 as position
        Stack to_search = new Stack();
        Hashtable searched = new Hashtable();

        
        to_search.Push(start_pos);

        //searched is Vector3 acting as pos, and 1 or 0 if seen
        searched.Add(start_pos, 1);


        return 0;
    }

    List<Vector3> getNeighbors(Vector3 start_pos)
    {
        int start_x = (int)start_pos.x;
        int start_y = (int)start_pos.y;

        List<Vector3> to_return = new List<Vector3>();

        //Get left
        if(start_x - 1 >= 0)
        {
            to_return.Add(new Vector3(start_x - 1, start_y));
        }
        //Get up
        if(start_y - 1 >= 0)
        {
            to_return.Add(new Vector3(start_x, start_y - 1));
        }
        //Get right
        if(start_x + 1 < columns)
        {
           to_return.Add(new Vector3(start_x + 1, start_y));
        }
        //Get Down
        if(start_y + 1 < rows)
        {
           to_return.Add(new Vector3(start_x, start_y + 1));
        }

        return to_return;
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
