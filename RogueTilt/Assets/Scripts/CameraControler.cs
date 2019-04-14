using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Camera MainCamera;

    Global global;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("GlobalObject").GetComponent<Global>();

        //Set the offset
        offset = transform.position - global.GetActiveTile().transform.position;
        Debug.Log("Camera controler has started");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void FixedUpdate()
    {
        MainCamera.transform.LookAt(global.GetActiveTile().transform);

        MainCamera.transform.position = global.GetActiveTile().transform.position + offset;

        //check all the keyboard input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            global.GetActiveTile().transform.Rotate(1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            global.GetActiveTile().transform.Rotate(-1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            global.GetActiveTile().transform.Rotate(0, 0, 1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            global.GetActiveTile().transform.Rotate(0, 0, -1f);
        }

    }
}
