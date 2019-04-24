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
            //create a new vector3 that will be the delta between the previous rotation and where we want to rotate to
            Vector3 to_add =  new Vector3();

            //check all the keyboard input to determine the deta of rotation
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                to_add.x += RotateRate;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                to_add.x += -1 * RotateRate;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                to_add.z += RotateRate;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                to_add.z += -1 * RotateRate;
            }

            //Get the previous rotation
            Vector3 PrevRotation = global.GetActiveTile().transform.localRotation.eulerAngles;

            //remove any roll
            PrevRotation.y = 0;

            //Ad the delta rotation to_add to the prev rotation to get the new rotation
            Vector3 new_rotation = PrevRotation + to_add;

            //Set the local rotation of the active tile as our new position
            global.GetActiveTile().transform.localRotation = Quaternion.Euler(new_rotation);
        }

        //Space bar to reset rotation
        if (Input.GetKey(KeyCode.Space))
        {
            global.ResetActiveTile();
        }

        //Get our curent rotation for checking it is out of bounds
        Vector3 currentRotation = global.GetActiveTile().transform.localRotation.eulerAngles;

        //Clamp our current rotation to the max amount
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
