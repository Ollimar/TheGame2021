﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    public PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        player.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraReady()
    {
        player.enabled = true;
        player.Fade();
        GetComponent<Camera>().depth = -1;
        gameObject.SetActive(false);
    }
}
