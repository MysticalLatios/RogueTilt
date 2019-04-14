using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Camera MainCamera;
    Tile active_tile;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //Set the offset
        offset = transform.position - active_tile.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.LookAt(active_tile);

        MainCamera.transform.position = active_tile.transform.position + offset;
    }
}
