using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static GameObject ActiveTile;

    public GameObject GetActiveTile()
    {
        //Debug.Log("GetActiveTile() has been called");
        return ActiveTile;
    }

    public Vector3 GetTilePosition()
    {
        return GetActiveTile().transform.position;
    }

    public void SetActiveTile(GameObject gameObject)
    {
        ActiveTile = gameObject;
        Debug.Log("The Active Tile has been set");
    }

    public void ResetActiveTile()
    {
        GetActiveTile().transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
