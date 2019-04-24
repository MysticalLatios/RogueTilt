using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float timetoTele = 0.25f;
    private float stayCount = 0.0f;
    bool enter_door = false;

    private Vector3 last_spawn_pos;

    Global global;

    public void SetLastSpawnPos(Vector3 pos)
    {
        last_spawn_pos = pos;
    }

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("GlobalObject").GetComponent<Global>();

        //Set default last spawn pos
        last_spawn_pos = global.GetActiveTile().transform.position;
        last_spawn_pos.y = 3.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Found Something");
        if (other.tag == "Door")
        {
            enter_door = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(enter_door)
        {
            //Debug.Log("In door");
            if (stayCount > timetoTele)
            {
                TeleportObject teleport = other.GetComponent<TeleportObject>();
                teleport.passObject(gameObject);
            }
            else
            {
                stayCount += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Left Door");
        if (other.tag == "Door")
        {
            enter_door = false;
        }
        stayCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (transform.position.y < -10)
        {
            global = GameObject.Find("GlobalObject").GetComponent<Global>();
            global.createNewManager();
            Destroy(gameObject);
        }
        */

        //Check if the ball is out of bounds
        if (transform.position.y < -10)
        {
            global.ResetActiveTile();
            gameObject.transform.position = last_spawn_pos;
        }
    }
}
