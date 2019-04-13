using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
   //public GameObject baseTile;
   //public List<GameObject> enemies;

    //Where the tile is on the grid
    public Vector3 position;

    public void SetPosition(Vector3 pos_in)
    {
        position = pos_in;
    }

    public Vector3 GetPosition()
    {
        return position;
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

        //Randomly assign the enemies

        //Set the position
        SetPosition(position_in);
    }



}
