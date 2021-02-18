using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{

    public GameObject player, spaceShip;
    public Camera cam;

    public int levelNumber;

    // Start is called before the first frame update
    void Start()
    {
        if(levelNumber == 4)
        {
            cam = Camera.main;
            //cam.GetComponent<CameraScript>().FlyCamera();
            player = GameObject.FindGameObjectWithTag("Player");
            spaceShip = GameObject.Find("SpaceShip");
            player.GetComponent<PlayerScript>().StartFlight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
