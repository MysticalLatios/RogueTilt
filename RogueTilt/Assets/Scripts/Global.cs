using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static GameObject ActiveTile;
    private LevelManeger levmanag;
   

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
        //If we already have an active tile reset its rotation
        if(ActiveTile != null)
        {
            ResetActiveTile();
        }
        
        ActiveTile = gameObject;
        Debug.Log("The Active Tile has been set");
    }

    public void SetActiveManager(LevelManeger manager)
    {
        levmanag = manager;
    }

    public void createNewManager()
    {
        levmanag.Reset();
    }

    public void ResetActiveTile()
    {
        //Reset the rotation of the tile
        GetActiveTile().transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        //Init it as itself, but this will get overittern very soon
        //ActiveTile = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
