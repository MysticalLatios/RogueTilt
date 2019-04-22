using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    //max tilt amount
    public int MaxDeg = 25;
    //How fast rotation happens
    public float RotateRate = 2f;

    //global for getting current tile
    Global global;

    //Gryro for devices that support it
    Gyroscope input_Gyro;

    Quaternion offset;

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

            offset = input_Gyro.attitude;
        }

        //If the device is a Handheld
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            //Lock the screen orientation
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Stop the screen from timing out
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }

    public void FixedUpdate()
    {
        //if system has a gryo use the gryo input
        if (SystemInfo.supportsGyroscope)
        {
            //Set the offset in start or some other function something
            Vector3 destroy_z_ofset = offset.eulerAngles;
            offset = Quaternion.Euler(destroy_z_ofset.x, 0, destroy_z_ofset.y);

            //Get gryo input
            Quaternion raw_input = input_Gyro.attitude;
            Vector3 destroy_z = raw_input.eulerAngles;

            //remove the z rotation(roll)
            Quaternion rotation = Quaternion.Euler(destroy_z.x, 0, destroy_z.y);
            //Set the rotation of the active tile with a ratio of (gyro) 0.8:1 (tile)
            global.GetActiveTile().transform.rotation = Quaternion.LerpUnclamped(offset, rotation, 0.8f);
        }

        //If no gryo use keyboard input
        else
        {
            //check all the keyboard input
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                global.GetActiveTile().transform.Rotate(RotateRate, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                global.GetActiveTile().transform.Rotate(-1 * RotateRate, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                global.GetActiveTile().transform.Rotate(0, 0, RotateRate);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                global.GetActiveTile().transform.Rotate(0, 0, -1f * RotateRate);
            }
        }

        //Space bar to reset rotation
        if (Input.GetKey(KeyCode.Space))
        {
            global.ResetActiveTile();
        }


        Vector3 currentRotation = global.GetActiveTile().transform.localRotation.eulerAngles;

        //Debug.Log(currentRotation.x.ToString());
        currentRotation.x = Mathf.Clamp(CorrectedRotation(currentRotation.x), (-1 * MaxDeg), MaxDeg);
        currentRotation.z = Mathf.Clamp(CorrectedRotation(currentRotation.z), (-1 * MaxDeg), MaxDeg);
        global.GetActiveTile().transform.localRotation = Quaternion.Euler(currentRotation);

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
