﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : TileBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        tileType = "regular";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Effect()
    {
        return;
    }
}