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
                // TO DO: increase chance of creating hole for everyone postion without hole
                if (Random.Range(1,10) != 1 
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

    void InitialiseMaze()
    {
        List<Vector3> TilesVisted = new List<Vector3>();
        Vector3 CurrentTile =  startTile.GetPosition();

        while (CurrentTile != exitTile.GetPosition())
        {
            //Add our current tile to where we have been
            TilesVisted.Add(CurrentTile);
            
            if (CurrentTile != startTile.GetPosition())
            {
                //Add the tile
                floorTiles.Add(new Tile(CurrentTile));
            }

            List<Vector3> NeighborTiles = new List<Vector3>();



            //Remove all tiles we have visited
            foreach (Vector3 Tile in TilesVisted)
            {
                if(NeighborTiles.Contains(Tile))
                {
                    NeighborTiles.Remove(Tile);
                }
                
            }


            //Set the next tile we are going to look at
            CurrentTile = NeighborTiles[Random.Range(0, NeighborTiles.Count - 1)];
        }

    }


    // Start is called before the first frame update
    // Init the board
    void Start()
    {
        //Set up the Tiles
        InitialiseList();

        for(int i =0; i < floorTiles.Count; i++)
        {
            Debug.Log("Tile at " + i + "Has the position: " + floorTiles[i].position.x + floorTiles[i].position.y);
        }

        //Set pup the maze
        InitialiseMaze();

        //Set the active Tile as the start tile

        activeTile = startTile;
    }


    List<Vector3> GetNeighbours(Tile tile_in)
    {
        List<Vector3> Neighbourhood = new List<Vector3>();
        List<Vector3> AllVectors = GetAllVectors();

        Vector3 NeighbourNorth = new Vector3(tile_in.GetPosition().x, tile_in.GetPosition().y -1, 0);
        Vector3 NeighbourEast = new Vector3(tile_in.GetPosition().x + 1, tile_in.GetPosition().y, 0);
        Vector3 NeighbourSouth = new Vector3(tile_in.GetPosition().x, tile_in.GetPosition().y + 1, 0);
        Vector3 NeighbourWest = new Vector3(tile_in.GetPosition().x - 1, tile_in.GetPosition().y, 0);

        AllVectors.Remove(startTile.GetPosition());

        if (AllVectors.Contains(NeighbourNorth))
        {
            Neighbourhood.Add(NeighbourNorth);
        }
        if (AllVectors.Contains(NeighbourEast))
        {
            Neighbourhood.Add(NeighbourEast);
        }
        if (AllVectors.Contains(NeighbourSouth))
        {
            Neighbourhood.Add(NeighbourSouth);
        }
        if (AllVectors.Contains(NeighbourWest))
        {
            Neighbourhood.Add(NeighbourWest);
        }


        return Neighbourhood;
    }

    List<Vector3> GetAllVectors()
    {
        List<Vector3> all_vectors = new List<Vector3>();

        foreach(Tile tile in floorTiles)
        {
            all_vectors.Add(tile.GetPosition());
        }

        return all_vectors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
