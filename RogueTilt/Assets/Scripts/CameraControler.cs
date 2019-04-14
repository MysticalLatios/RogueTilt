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

        //check all the keyboard input
        if (Input.GetKey(KeyCode.UpArrow))
        {

            global.GetActiveTile().transform.Rotate(RotateRate, 0, 0);
            
            
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {

            global.GetActiveTile().transform.Rotate(-1 * RotateRate, 0, 0);
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            global.GetActiveTile().transform.Rotate(0, 0, RotateRate);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
    
            global.GetActiveTile().transform.Rotate(0, 0, -1f * RotateRate);
        
        }

        Vector3 currentRotation = global.GetActiveTile().transform.localRotation.eulerAngles;
        currentRotation.x = Mathf.Clamp(currentRotation.x, MaxDeg * -1, MaxDeg);
        currentRotation.z = Mathf.Clamp(currentRotation.z, MaxDeg * -1, MaxDeg);
        global.GetActiveTile().transform.localRotation = Quaternion.Euler(currentRotation);

    }

  
}
