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
    public List<Tile> floorTiles = new List<Tile>(); //All the other tiles
    

    //Possible grid positions
    private List<Vector3> gridPositions = new List<Vector3>();


    //Put the cords in the grid
    void InitialiseList()
    {
        //Make sure its empty
        gridPositions.Clear();

        //Place start tile
        startTile = new Tile(new Vector3((columns / 2), (rows / 2), 0), "Start");

        //Place end tile
        InitialiseEndTile();

        //x axis, columns
        for (int x = 0; x < columns; x++)
        {

            // y axis, rows
            for (int y = 0; y < rows; y++)
            {
                if (Random.Range(1,20) != 1 
                    && !(x == startTile.GetPosition().x && y == startTile.GetPosition().y)
                    && !(x == exitTile.GetPosition().x && y == exitTile.GetPosition().y)) 
                {
                    Vector3 position = new Vector3(0, 0, 0);

                    //change position
                    position.Set(x, y, 0);

                    // Put the x and y into the vector 3
                    gridPositions.Add(position);

                    floorTiles.Add(new Tile(position));
                }
                else
                {
                    Debug.Log("Position at: " + x + " " + y + "Not created");
                }
            }
        }
    }

    void InitialiseEndTile()
    {
        int location = Random.Range(1, 4);
        
        switch (location)
        {
            case 1:
                exitTile = new Tile(new Vector3((0), (0), 0), "End");
                break;

            case 2:
                exitTile = new Tile(new Vector3((0), (rows), 0), "End");
                break;

            case 3:
                exitTile = new Tile(new Vector3((columns), (0), 0), "End");
                break;

            case 4:
                exitTile = new Tile(new Vector3((columns), (rows), 0), "End");
                break;

            default:
                break;
        }
            

    }


    // Start is called before the first frame update
    // Init the board
    void Start()
    {
        InitialiseList();

        for(int i =0; i < floorTiles.Count; i++)
        {
            Debug.Log("Tile at " + i + "Has the position: " + floorTiles[i].position.x + floorTiles[i].position.y);
        }

        //Assign the center tile as the start tile

        //Set the active Tile as the start tile
        
        activeTile = startTile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
