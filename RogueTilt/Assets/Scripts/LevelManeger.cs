using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManeger : MonoBehaviour
{
    // Vars for our Level
    private static int columns = 6;
    private static int rows = 6;
    public GameObject exitTile;
    private Vector3 end_pos;
    private GameObject startTile;
    private Vector3 start_pos;
    private Global global;
    public List<List<GameObject>> floorTiles = new List<List<GameObject>>(); //All the other tiles

    int offset = 60;

    //How far can start and end be?
    int miniDistance = 2;

    public void Reset()
    {
        global = GameObject.Find("GlobalObject").GetComponent<Global>();

        //Set up the Tiles
        InitialiseList();

        //Check if dfs can find its way out of the loop, if so, run InitialiseList() again

        //Set the active Tile as the start tile
        global.SetActiveTile(startTile);

        AssignNeighbors();

        global.SetActiveManager(this);
    }

    void ClearTileList()
    {
        foreach (List<GameObject> rows in floorTiles)
        {
            foreach (GameObject tile in rows)
            {
                Destroy(tile);
            }
        }
        floorTiles.Clear();
    }

    void createMap()
    {
        //Make sure its empty
        ClearTileList();

        //x axis, columns
        for (int x = 0; x < columns; x++)
        {
            floorTiles.Add(new List<GameObject>());
            // y axis, rows
            for (int y = 0; y < rows; y++)
            {
                // HERE
                if (Random.Range(1, 100) > 50)
                // THE CHANCE

                {
                    floorTiles[x].Add(null);
                    //Debug.Log("Position at: " + x + " " + y + "Not created");
                }

                else
                {
                    floorTiles[x].Add(Instantiate(pickTile()));
                    floorTiles[x][y].transform.position = new Vector3((x + 1) * offset, 0, (y + 1) * offset);
                    //Debug.Log("Position at: " + x + " " + y + "Created");
                }
            }
        }
    }

    private void InitialiseStart()
    {
        
        start_pos = new Vector3(Random.Range(0, columns - 1), Random.Range(0, rows - 1), 0);
        //clean old tile
        Destroy(floorTiles[(int)start_pos.x][(int)start_pos.y]);
        startTile = Instantiate(Resources.Load<GameObject>("Prefabs/SpecialTile/Start Room"));
        startTile.transform.position = new Vector3((start_pos.x + 1) * offset, 0, (start_pos.y + 1) * offset);
        floorTiles[(int)start_pos.x][(int)start_pos.y] = startTile;
    }

    private void IntialiseEnd()
    {
        end_pos = new Vector3(Random.Range(0, columns - 1), Random.Range(0, rows - 1), 0);
        //ToDo: Make sure they are farther apart

        bool notFarEnough = (isNotFarEnough(start_pos, end_pos, miniDistance));
        //Debug.Log("Yo we did the dfs and the result is:" + canFind);

        int i = 0;
        int compromiseDistance = miniDistance;
        while (end_pos == start_pos || notFarEnough )
        {
            i++;
            //Debug.Log("Rerolling,  end_pos == start_pos:" + (end_pos == start_pos) + " notFarEnough:" + notFarEnough);

            end_pos = new Vector3(Random.Range(0, columns - 1), Random.Range(0, rows - 1), 0);
            //Give up tring to find a min distance after X number of tries
            if (i % 10 == 0)
            {
                compromiseDistance--;
                //canFind = false;
                //Debug.Log("Compromising...");
            }
            else
            {
                notFarEnough = (isNotFarEnough(start_pos, end_pos, compromiseDistance));
            }
        }
        //Debug.Log((notFarEnough) + " " + end_pos.x + "," + end_pos.y + " " + start_pos.x + "," + start_pos.y);
        //clean old tile
        Destroy(floorTiles[(int)end_pos.x][(int)end_pos.y]);
        exitTile = Instantiate(Resources.Load<GameObject>("Prefabs/SpecialTile/End Room"));
        exitTile.transform.position = new Vector3((end_pos.x + 1) * offset, 0, (end_pos.y + 1) * offset);
        floorTiles[(int)end_pos.x][(int)end_pos.y] = exitTile;
    }

    //Put the cords in the grid
    void InitialiseList()
    {
        createMap();

        //Set start tile
        InitialiseStart();

        //Set end tile
        IntialiseEnd();
    }

    GameObject pickTile()
    {
        Object[] TileContianer = Resources.LoadAll("Prefabs/Tiles/", typeof(GameObject));

        int index = Random.Range(0, TileContianer.Length);

        GameObject selected_tile = (GameObject)TileContianer[index];

        return selected_tile;
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

    //Search through our grid to make sure we can find the exit, also destroy places we cant get to
    private bool BreadthFirstSearch(Vector3 start_pos, Vector3 to_find)
    {
        bool found = false;

        //Queue contains the Vector3 as position
        Queue<Vector3> to_search = new Queue<Vector3>();
        Dictionary<Vector3, int> searched = new Dictionary<Vector3, int>();

        //Init all things in searched as false, 0
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (floorTiles[x][y] != null)
                {
                    searched[new Vector3(x,y)] = 0;
                }
            }
        }

        to_search.Enqueue(start_pos);

        //searched is Vector3 acting as pos, and 1 or 0 if seen
        searched[start_pos] = 1;
        
        //Get first front
        Vector3 front = to_search.Dequeue();

        //Get The neighbors and add them to Queue
        foreach (Vector3 vector in getNeighbors(front))
        {
            to_search.Enqueue(vector);
        }

        while (to_search.Count > 0)
        {
            //Get front vector in queue
            front = to_search.Dequeue();

            if (front == to_find) found = true;

            if (searched[front] == 1) { } // Already found so move on

            else
            {
                //Who is this handsome fellow? (Haven't seen before)
                searched[front] = 1;
                foreach (Vector3 vector in getNeighbors(front))
                {
                    to_search.Enqueue(vector);
                }
            }

        }

        //Destory all squares we cant go to save some resources
        foreach(KeyValuePair<Vector3, int> entry in searched)
        {
            if (entry.Value == 0)
            {
                Destroy(floorTiles[((int)entry.Key.x)][(int)entry.Key.y]);
                floorTiles[((int)entry.Key.x)][(int)entry.Key.y] = null;
            }
        }


        //Return if we found it or not
        return found;
    }

    List<Vector3> getNeighbors(Vector3 start_pos)
    {
        int start_x = (int)start_pos.x;
        int start_y = (int)start_pos.y;
        //Debug.Log("Getting Neighbors");
        List<Vector3> to_return = new List<Vector3>();

        //Get left
        if(start_x - 1 >= 0 && floorTiles[start_x - 1][start_y] != null)
        {
            to_return.Add(new Vector3(start_x - 1, start_y));
        }
        //Get up
        if(start_y - 1 >= 0 && floorTiles[start_x][start_y -1] != null)
        {
            to_return.Add(new Vector3(start_x, start_y - 1));
        }
        //Get right
        if(start_x + 1 < columns && floorTiles[start_x + 1][start_y] != null)
        {
           to_return.Add(new Vector3(start_x + 1, start_y));
        }
        //Get Down
        if(start_y + 1 < rows && floorTiles[start_x][start_y + 1] != null)
        {
           to_return.Add(new Vector3(start_x, start_y + 1));
        }
        
        if(to_return.Count == 0)
        {
            //we where unable to find any Neighbors
            Debug.Log("The tile at: " + start_x + "," + start_y + " Does not have any neighboors" );
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
        bool cantfind = !(BreadthFirstSearch(start_pos, end_pos));


        int j = 0;
        while(cantfind && j < 6)
        {
            j++;
            InitialiseList();
            Debug.Log("Rerolling map");
            cantfind = !(BreadthFirstSearch(start_pos, end_pos));
        }
        //Spawn the player ball
        spawnBall(start_pos);

        //Set the active Tile as the start tile
        global.SetActiveTile(startTile);

        AssignNeighbors();
    }

    //spawn player ball in start tile
    void spawnBall(Vector3 start_pos)
    {
        GameObject Player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        Player.transform.position = new Vector3((start_pos.x + 1) * offset, 2f, (start_pos.y + 1) * offset);

    }

    // Update is called once per frame
    void Update()
    {

    }


    void GetDoorExit(GameObject exitRoom, string doorName, TeleportObject me)
    {
        if (exitRoom != null)
        {
            Transform[] PossibleExits = exitRoom.GetComponentsInChildren<Transform>();
            foreach (Transform exit in PossibleExits)
            {
                if (exit.name == doorName)
                {
                    me.setExit(exit.gameObject);
                }

            }
        }
        else
        {
            me.setExit(null);
        }

    }



    //Assign each tile its neighboors
    void AssignNeighbors()
    {
        // x axis
        for (int x = 0; x < columns; x++)
        {
            // y axis, rows
            for (int y = 0; y < rows; y++)
            {
                GameObject current_tile = floorTiles[x][y];

                if(current_tile != null)
                {
                    //Debug.Log("The name of this tile is:" + current_tile.name);

                    Transform[] current_door_transforms = current_tile.GetComponentsInChildren<Transform>();

                    foreach (Transform child in current_door_transforms)
                    {
                        if (child.name.Contains("Door"))
                        {
                            //Debug.Log("We got this door:" + child.name + " That belongs to: " + current_tile.name);
                            GameObject nieghboor;

                            TeleportObject telport_script = child.GetComponent<TeleportObject>();

                            //Switch on the first char of the door, ex N door
                            switch(child.name[0])
                            {
                                case 'N':
                                    //Assign the room that the north door would teleport to
                                    //check if in range of grid
                                    if (y + 1 < rows)
                                    {
                                        nieghboor = floorTiles[x][y + 1];

                                        GetDoorExit(nieghboor, "S Door",telport_script);
                                        telport_script.assignDir(TeleportObject.Direction.NORTH);

                                        //telport_script.AssignRoom(nieghboor);
                                        //Debug.Log("Assigned a room to a door");

                                    }

                                    else
                                    {
                                        telport_script.setExit(null);
                                    }

                                    break;

                                case 'E':
                                    //Assign the room that the north door would teleport to
                                    //check if in range of grid
                                    if (x + 1 < rows)
                                    {
                                        nieghboor = floorTiles[x + 1][y];


                                        GetDoorExit(nieghboor, "W Door",telport_script);

                                        //telport_script.AssignRoom(nieghboor);

                                        telport_script.assignDir(TeleportObject.Direction.EAST);

                                        Debug.Log("Assigned a room to a door");

                                    }

                                    else
                                    {
                                        telport_script.setExit(null);
                                    }

                                    break;

                                case 'S':
                                    //Assign the room that the north door would teleport to
                                    if (y - 1 >= 0)
                                    {
                                        nieghboor = floorTiles[x][y - 1];


                                        GetDoorExit(nieghboor, "N Door", telport_script);


                                        //telport_script.AssignRoom(nieghboor);
                                        telport_script.assignDir(TeleportObject.Direction.SOUTH);

                                        //Debug.Log("Assigned a room to a door");

                                    }

                                    else
                                    {
                                        telport_script.setExit(null);
                                    }

                                    break;

                                case 'W':
                                    //Assign the room that the north door would teleport to
                                    if (x - 1 >= 0)
                                    {
                                        nieghboor = floorTiles[x - 1][y];


                                        GetDoorExit(nieghboor, "E Door", telport_script);


                                        //telport_script.AssignRoom(nieghboor);
                                        telport_script.assignDir(TeleportObject.Direction.WEST);

                                        //Debug.Log("Assigned a room to a door");
                                    }

                                    else
                                    {
                                        telport_script.setExit(null);
                                    }
                                    break;

                                default:
                                    break;
                            }                         
                        }
                    }
                }
            }
        }
    }
}

