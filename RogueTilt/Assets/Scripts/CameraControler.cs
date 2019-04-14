using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject ActiveTile;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //Set the offset
        offset = transform.position - ActiveTile.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.LookAt(ActiveTile.transform);

        MainCamera.transform.position = ActiveTile.transform.position + offset;
    }

    public void UpdateActiveTile(GameObject tile)
    {
        ActiveTile = tile;
        Debug.Log("Active tile updated on CameraControler!");
    }

    public void FixedUpdate()
    {
        //check all the keyboard input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ActiveTile.transform.Rotate(1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ActiveTile.transform.Rotate(-1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ActiveTile.transform.Rotate(0, 0, 1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ActiveTile.transform.Rotate(0, 0, -1f);
        }

    }
}
