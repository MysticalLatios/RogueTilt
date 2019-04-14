using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
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


    void passObject(char direction)
    {

    }
}
