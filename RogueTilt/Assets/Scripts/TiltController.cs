using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    //max tilt amount
    public int MaxDeg = 20;
    //How fast rotation happens
    public float RotateRate = 2f;

    //global for getting current tile
    Global global;

    //Gryro for devices that support it
    Gyroscope input_Gyro;


    // Start is called before the first frame update
    void Start()
    {
        //init global
        global = GameObject.Find("GlobalObject").GetComponent<Global>();

        //Set up gyro and enable it if device has one
        if (SystemInfo.supportsGyroscope)
        {
            input_Gyro = Input.gyro;
            input_Gyro.enabled = true;
        }

    }

    public void FixedUpdate()
    {
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

        //Debug.Log(currentRotation.x.ToString());
        currentRotation.x = Mathf.Clamp(CorrectedRotation(currentRotation.x), -20, 20);
        currentRotation.z = Mathf.Clamp(CorrectedRotation(currentRotation.z), -20, 20);
        global.GetActiveTile().transform.localRotation = Quaternion.Euler(currentRotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //CorrectedRotation for handling
    float CorrectedRotation(float currentRotation)
    {
        if (currentRotation > 180)
        {
            currentRotation -= 360;
        }

        return currentRotation;
    }


}
