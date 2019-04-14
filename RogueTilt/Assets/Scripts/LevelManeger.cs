using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManeger : MonoBehaviour
{
    // Vars for our Level
    public int columns = 6;
    public int rows = 6;
    public Tile exitTile;
    private Tile startTile;
    public Tile activeTile;
    public List<List<Tile>> floorTiles = new List<List<Tile>>(); //All the other tiles


    //Put the cords in the grid
    void InitialiseList()
    {
        //Make sure its empty
        floorTiles.Clear();

        //x axis, columns
        for (int x = 0; x < columns; x++)
        {
            floorTiles.Add(new List<Tile>());
            // y axis, rows
            for (int y = 0; y < rows; y++)
            {
                if (Random.Range(1, 8) == 1)
                {
                    floorTiles[x].Add(null);
                    Debug.Log("Position at: " + x + " " + y + "Not created");
                }

                else
                {
                    floorTiles[x].Add(new Tile(new Vector3(x, y, 0)));
                    Debug.Log("Position at: " + x + " " + y + "Created");
                }
            }
        }

        //Set start tile
        Vector3 start_pos = new Vector3(Random.Range(0, columns -1), Random.Range(0, rows-1), 0);
        startTile = new Tile(start_pos, "Start");
        floorTiles[(int)start_pos.x][(int)start_pos.y] = startTile;

        //Set end tile
        Vector3 end_pos = new Vector3(Random.Range(0, columns -1), Random.Range(0, rows -1), 0);
        //ToDo: Make sure they are farther apart
        while(end_pos == start_pos)
        {
            end_pos = new Vector3(Random.Range(0, columns - 1), Random.Range(0, rows - 1), 0);
        }
        exitTile = new Tile(end_pos, "Start");
        floorTiles[(int)end_pos.x][(int)end_pos.y] = exitTile;

    }


    // Start is called before the first frame update
    // Init the board
    void Start()
    {
        //Set up the Tiles
        InitialiseList();

        //Check if dfs can find its way out of the loop, if so, run InitialiseList() again

        //Set the active Tile as the start tile
        activeTile = startTile;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
