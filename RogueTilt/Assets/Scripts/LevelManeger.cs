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

        

        //x axis, columns
        for (int x = 1; x < columns - 1; x++)
        {

            // y axis, rows
            for (int y = 1; y < rows - 1; y++)
            {
                Vector3 position = new Vector3(0, 0, 0);

                //change position
                position.Set(x, y, 0);
                
                // Put the x and y into the vector 3
                gridPositions.Add(position);

                floorTiles.Add(new Tile(position));
                
            }
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
