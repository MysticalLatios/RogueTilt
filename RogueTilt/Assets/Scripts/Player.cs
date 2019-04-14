using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float timetoTele = 0.25f;
    private float stayCount = 0.0f;
    bool enter_door = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Found Something");
        if (other.tag == "Door")
        {
            enter_door = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(enter_door)
        {
            Debug.Log("In door");
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
        Debug.Log("Left Door");
        if (other.tag == "Door")
        {
            enter_door = false;
        }
        stayCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
