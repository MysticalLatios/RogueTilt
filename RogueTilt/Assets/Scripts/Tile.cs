using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject baseTile = null;
    //public List<GameObject> enemies;
    public List<GameObject> tilePrefabs;
    //Where the tile is on the grid
    public Vector3 position;
    private int offset = 15;



    private void Start()
    {
        GameObject[] tilePrefabsArray = Resources.LoadAll<GameObject>("Prefabs/Tiles");
        foreach(GameObject tiletype in tilePrefabsArray)
        {
            Debug.Log("Added prefab");
            tilePrefabs.Add(tiletype);
        }

    }

    public void SetPosition(Vector3 pos_in)
    {
        position = pos_in;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    private void PlaceSelf()
    {
        //Assign it's tile type from the list of prefabs


        //place according to it's postion
        //Each tile is 26 X 26
        //The center is the position because that's how scale be, we need to, at the very least off set each tile by 13
        //We considered the idea that girds should be 30 x 30, so offset by 15 for placement

        baseTile = Instantiate(tilePrefabs[0]) as GameObject;

        baseTile.transform.position = new Vector3((position.x + 1) * offset, 0, (position.z + 1) * offset);

    }

    //Tile constructor
    public Tile(Vector3 position_in)
    {
        //Assign our baseTile from a list of prefabs
        Debug.Log("Got to tile");
        //Randomly assign the enemies
        //Set the position
        SetPosition(position_in);
        
         PlaceSelf();
       
    }

    public Tile(Vector3 position_in, string tile_name)
    {
        //Assign our baseTile based on the tile_name string

        //hard coded to pick base tile currently
        Debug.Log("Got to tile");
        //Set the position in the grid 
        SetPosition(position_in);
       
         PlaceSelf();
        
    }



}
