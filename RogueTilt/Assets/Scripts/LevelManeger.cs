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
    public List<Tile> floorTiles; //All the other tiles
    

    //Possible grid positions
    private List<Vector3> gridPositions = new List<Vector3>();


    //Put the cords in the grid
    void InitialiseList()
    {
        //Make sure its empty
        gridPositions.Clear();

        //x axis, columns
        for (int x = 1; x < columns - 1; x++)
        {

            // y axis, rows
            for (int y = 1; y < rows - 1; y++)
            {
                // Put the x and y into the vector 3
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    // Start is called before the first frame update
    // Init the board
    void Start()
    {
        //Assign the center tile as the start tile

        //Set the active Tile as the start tile
        
        activeTile = startTile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
