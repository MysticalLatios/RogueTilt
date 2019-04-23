using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{

    public enum Direction {NORTH,EAST,SOUTH,WEST}

    Global global;

    public Direction my_dir;

    private float OFFSET = 2.0f;

    private GameObject Exit;

    //the player, hopefully
    private GameObject to_pass;

    //room_to_go should be the whole prefab
    private GameObject room_to_go = null;

    

    // Start is called before the first frame update

    private void Awake()
    {
        gameObject.SetActive(true);
    }


    public void setExit(GameObject Exit)
    {
        this.Exit = Exit;
        if (this.Exit == null)
        {
            gameObject.SetActive(false);
        }
    }


    public void assignDir(Direction mydir)
    {
        my_dir = mydir;
    }


    public void AssignRoom(GameObject room_to_assign)
    {
        room_to_go = room_to_assign;
        if(room_to_go != null)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void passObject(GameObject to_move)
    {

        //Debug.Log("Got into pass Object");

        global = GameObject.Find("GlobalObject").GetComponent<Global>();

        // get the door to move to
        //we have the whole tile 
        //Debug.Log("Trying to tele to: " + Exit.);
        Vector3 new_pos = new Vector3(); 
        //new_pos = room_to_go.transform.position;
        //ew_pos.y = 3.0f;
        switch (my_dir)
        {
            case Direction.NORTH:
                new_pos = new Vector3(Exit.transform.position.x, 4.0f, Exit.transform.position.z+OFFSET);
                break;
            case Direction.EAST:
                new_pos = new Vector3(Exit.transform.position.x +OFFSET, 4.0f, Exit.transform.position.z);
                break;
            case Direction.SOUTH:
                new_pos = new Vector3(Exit.transform.position.x, 4.0f, Exit.transform.position.z-OFFSET);
                break;
            case Direction.WEST:
                new_pos = new Vector3(Exit.transform.position.x -OFFSET, 4.0f, Exit.transform.position.z);
                break;

        }

        to_move.transform.position = new_pos;

        // we are getting the top level parent object form out structure of door->wall->room
        // this is also flawless code.
        global.SetActiveTile(Exit.transform.parent.parent.gameObject);

    }
}
