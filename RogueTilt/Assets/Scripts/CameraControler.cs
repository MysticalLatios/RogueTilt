using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Camera MainCamera;
    Tile ActiveTile;

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

    public void UpdateActiveTile(Tile tile)
    {
        ActiveTile = tile;
    }
}
