﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenTurnipScript : MonoBehaviour
{

    public int turnipNumber;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
