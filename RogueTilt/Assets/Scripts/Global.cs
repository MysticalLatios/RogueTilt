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

    public void SetActiveTile(GameObject gameObject)
    {
        ActiveTile = gameObject;
        Debug.Log("The Active Tile has been set");
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
