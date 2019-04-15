using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Camera MainCamera;
    public int MaxDeg = 20;
    public float RotateRate = 2f;

    Global global;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("GlobalObject").GetComponent<Global>();

        //Set the offset
        offset = new Vector3(0, 30, -20);
        Debug.Log("Camera controler has started");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void FixedUpdate()
    {
        MainCamera.transform.position = global.GetActiveTile().transform.position + offset;

        MainCamera.transform.LookAt(global.GetActiveTile().transform);

    }

  
}
