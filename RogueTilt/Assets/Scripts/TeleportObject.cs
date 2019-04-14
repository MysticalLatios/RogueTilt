﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    Global global;
    //the player, hopefully
    private GameObject to_pass;

    //room_to_go should be the whole prefab
    private GameObject room_to_go = null;

    // Start is called before the first frame update

    private void Awake()
    {
        gameObject.SetActive(true);
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
       

        global = GameObject.Find("GlobablObject").GetComponent<Global>();

        // get the door to move to
        //we have the whole tile 
        Vector3 new_pos = to_pass.transform.position;
        new_pos.y = 2.0f;
        to_move.transform.position = new_pos;

        global.SetActiveTile(room_to_go);

    }
}
