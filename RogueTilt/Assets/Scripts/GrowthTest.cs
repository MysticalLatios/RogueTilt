﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthTest : MonoBehaviour
{
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if((i % 10) == 0)
        {
            transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
        }
        i++;
        

    }
}
