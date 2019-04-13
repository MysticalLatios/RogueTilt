using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManeger : MonoBehaviour
{
    // Vars for our Level
    public int columns = 6;
    public int rows = 6;
    public GameObject exit;
    public GameObject[] floorTiles;

    //Possible grid positions
    private List<Vector3> gridPositions = new List<Vector3>();


    // Start is called before the first frame update
    // Init the board
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
