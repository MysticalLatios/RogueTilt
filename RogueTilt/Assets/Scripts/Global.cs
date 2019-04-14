using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static GameObject ActiveTile;

    public static GameObject GetActiveTile()
    {
        return ActiveTile;
    }

    public void SetActiveTile(GameObject gameObject)
    {
        ActiveTile = gameObject;
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
