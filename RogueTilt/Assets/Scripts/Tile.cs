using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject baseTile;
    //public List<GameObject> enemies;
    public List<GameObject> tilePrefabs;
    //Where the tile is on the grid
    public Vector3 position;
    private int offset = 15;

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
        baseTile = tilePrefabs[0];

        //place according to it's postion
        //Each tile is 26 X 26
        //The center is the position because that's how scale be, we need to, at the very least off set each tile by 13
        //We considered the idea that girds should be 30 x 30, so offset by 15 for placement

        Instantiate(baseTile, new Vector3((position.x + 1) * offset, 0, (position.z + 1) * offset), Quaternion.identity);

    }

    //Tile constructor
    public Tile(Vector3 position_in)
    {
        //Assign our baseTile from a list of prefabs

        //Randomly assign the enemies

        //Set the position
        SetPosition(position_in);
    }

    public Tile(Vector3 position_in, string tile_name)
    {
        //Assign our baseTile based on the tile_name string

        //hard coded to pick base tile currently

        //Set the position in the grid 
        SetPosition(position_in);
    }



}
