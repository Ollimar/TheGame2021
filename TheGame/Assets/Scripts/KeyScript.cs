using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    public GameObject door;   
    public TipuToRescue tipuToRescue;
    public Rigidbody cageBottom;
    public Camera doorCamera;

    private PlayerScript player;
    private CameraScript cameraScript;

    private void Start()
    {
        player      = GameObject.Find("Player").GetComponent<PlayerScript>();
        cameraScript = Camera.main.GetComponent<CameraScript>();
        cameraScript.doorCamera = doorCamera;
        doorCamera = GameObject.Find("Door Camera").GetComponent<Camera>();
        doorCamera.GetComponent<Camera>().enabled = false;
    }

    public void Open()
    {
        doorCamera.GetComponent<Camera>().enabled = true;
        player.canMove = false;
        Camera.main.depth = -1;
        doorCamera.depth = 0;
        if(cageBottom != null)
        {
            cageBottom.useGravity = true;
            cageBottom.isKinematic = false;
        }
        tipuToRescue.Rescued();
        door.GetComponent<CageHolderScript>().enabled = true;
        cameraScript.StartCoroutine("DoorToPlayer");
    }
}
