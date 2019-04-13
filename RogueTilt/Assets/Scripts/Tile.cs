using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public GameObject baseTile;
    public List<GameObject> enemies;

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



}
