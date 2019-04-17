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
        //if system has a gryo use the gryo input
        if (SystemInfo.supportsGyroscope)
        {
            Vector3 absolute_transform = input_Gyro.attitude.eulerAngles;

            //ToDo: Make rotation more sensitve by taking half of the rotation, but make sure to correct the rotation first?
            //Quaternions are a bit strange so double check everything https://i.redd.it/vqivxqqxpts21.jpg
            global.GetActiveTile().transform.rotation = Quaternion.Euler(absolute_transform.x, 0, absolute_transform.y);
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
