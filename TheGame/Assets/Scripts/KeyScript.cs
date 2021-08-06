using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    public GameObject door;
    public Camera doorCamera;

    private PlayerScript player;
    private CameraScript cameraScript;

    private void Start()
    {
        player      = GameObject.Find("Player").GetComponent<PlayerScript>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    public void Open()
    {
        cameraScript.doorCamera = doorCamera;
        player.canMove = false;
        Camera.main.depth = -1;
        doorCamera.depth = 0;
        door.GetComponent<Animator>().enabled = true;
        cameraScript.StartCoroutine("DoorToPlayer");
    }
}
