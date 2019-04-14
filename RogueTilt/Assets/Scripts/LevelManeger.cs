using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManeger : MonoBehaviour
{
    // Vars for our Level
    private int columns = 5;
    private int rows = 5;
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
                    floorTiles[x].Add(Instantiate(pickTile()));
                    floorTiles[x][y].transform.position = new Vector3((x + 1) * offset, 0, (y + 1) * offset);
                   //Debug.Log("Position at: " + x + " " + y + "Created");
                }
            }
        }

        //Set start tile
        Vector3 start_pos = new Vector3(Random.Range(0, columns -1), Random.Range(0, rows-1), 0);
        //clean old tile
        Destroy(floorTiles[(int)start_pos.x][(int)start_pos.y]);
        startTile = Instantiate(Resources.Load<GameObject>("Prefabs/SpecialTile/Start Room"));
        startTile.transform.position = new Vector3((start_pos.x + 1) * offset, 0, (start_pos.y + 1) * offset);
        floorTiles[(int)start_pos.x][(int)start_pos.y] = startTile;

        //Set end tile
        Vector3 end_pos = new Vector3(Random.Range(0, columns -1), Random.Range(0, rows -1), 0);
        //ToDo: Make sure they are farther apart

        bool notFarEnough = (isNotFarEnough(start_pos, end_pos, miniDistance));
        //bool canFind = !(depthFirstSearch(start_pos, end_pos));
        

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
                //canFind = false;
                Debug.Log("Compromising...");
            }
            else
            {
                notFarEnough = (isNotFarEnough(start_pos, end_pos, compromiseDistance));
                //canFind = !(depthFirstSearch(start_pos, end_pos));
            }
            
        }
        Debug.Log((notFarEnough) + " " + end_pos.x + "," + end_pos.y + " " + start_pos.x + "," + start_pos.y);
        //clean old tile
        Destroy(floorTiles[(int)end_pos.x][(int)end_pos.y]);
        exitTile = Instantiate(Resources.Load<GameObject>("Prefabs/SpecialTile/End Room"));
        exitTile.transform.position = new Vector3((end_pos.x + 1) * offset, 0, (end_pos.y + 1) * offset);
        floorTiles[(int)end_pos.x][(int)end_pos.y] = exitTile;

        spawnBall(start_pos);
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


    //find the end from the start
    bool depthFirstSearch(Vector3 start_pos, Vector3 to_find)
    {
        //Stack contains the Vector3 as position
        Stack<Vector3> to_search = new Stack<Vector3>();
        Dictionary<Vector3, int> searched = new Dictionary<Vector3, int>();
        
        
        to_search.Push(start_pos);
        
        //searched is Vector3 acting as pos, and 1 or 0 if seen
        searched[start_pos] = 1;
        
        //Get The neighbors and add them too stack
        Vector3 top = to_search.Pop();

        addListToStack(getNeighbors(top), ref to_search);
        
        while (to_search.Count > 0)
        {
            top = to_search.Pop();
            Debug.Log("Looking at pos: " + top.ToString());
            if (top == to_find)
            {
                return true;
            }
            if (searched.ContainsKey(top))
            {
               
                //Already found just move on
            }
            else
            {
                //Who is this handsome fellow? (Haven't seen before)
                searched[top] = 1;
                addListToStack(getNeighbors(top), ref to_search);
            }
            
        }
        
        //didn't hit the return couldn't find it
        return false;
    }

    void addListToStack(List<Vector3> to_add,ref Stack<Vector3> muh_stack)
    {
        foreach (Vector3 vector in to_add)
        {
            muh_stack.Push(vector);
            Debug.Log("Pushed to stack: " + vector.ToString());
        }
    }

    List<Vector3> getNeighbors(Vector3 start_pos)
    {
        int start_x = (int)start_pos.x;
        int start_y = (int)start_pos.y;
        Debug.Log("Getting Neighbors");
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

        AssignNeighbors();
    }

    //spawn player ball in start tile
    void spawnBall(Vector3 start_pos)
    {
        GameObject Player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        Player.transform.position = new Vector3((start_pos.x + 1) * offset, 1.5f, (start_pos.y + 1) * offset);

    }

    // Update is called once per frame
    void Update()
    {
        
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
                    Debug.Log("The name of this tile is:" + current_tile.name);

                    Transform[] current_door_transforms = current_tile.GetComponentsInChildren<Transform>();

                    foreach (Transform child in current_door_transforms)
                    {
                        if (child.name.Contains("Door"))
                        {
                            Debug.Log("We got this door:" + child.name +" That belongs to: " + current_tile.name);

                            if(child.name[0] == 'N')
                            {
                                //Assign the room that the north door would teleport to
                            }

                            else if (child.name[0] == 'E')
                            {
                                //Assign the room that the north door would teleport to
                            }

                            else if (child.name[0] == 'S')
                            {
                                //Assign the room that the north door would teleport to
                            }

                            else if (child.name[0] == 'W')
                            {
                                //Assign the room that the north door would teleport to
                            }
                        }
                    }
                }
            }
        }
    }
}
