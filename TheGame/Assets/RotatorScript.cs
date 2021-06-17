using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    private Vector3 currentEulerAngles;

    public bool canMove = true;

    public float speed = 5f;
    public float turnSmoothing = 5f;

    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    public GameObject ship;
    public GameObject shipCollider;
    private Rigidbody myRB;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        // These if statements check what level player has returned from and starts the map level next to it.

        if(gm.previousLevel == 1)
        {
            transform.eulerAngles = new Vector3(18.985f, 64.897f, 4.446f);
            ship.transform.position = shipCollider.transform.position;
        }

        if (gm.previousLevel == 2)
        {
            transform.eulerAngles = new Vector3(48.813f, 40.348f, 17.294f);
            ship.transform.position = shipCollider.transform.position;
        }

        if (gm.previousLevel == 3)
        {
            transform.eulerAngles = new Vector3(5.775f, -14.521f, -69.05701f);
            ship.transform.position = shipCollider.transform.position;
        }

        if (gm.previousLevel == 4)
        {
            transform.eulerAngles = new Vector3(25.875f, 149.285f, -102.263f);
            ship.transform.position = shipCollider.transform.position;
        }

        if (gm.previousLevel == 5)
        {
            transform.eulerAngles = new Vector3(-6.634f, 70.78001f, -173.567f);
            ship.transform.position = shipCollider.transform.position;
        }

        if (gm.previousLevel == 11)
        {
            transform.eulerAngles = new Vector3(7.109f, -14.533f, -22.671f);
            ship.transform.position = shipCollider.transform.position;
        }

        else if (gm.previousLevel == 0 || gm.previousLevel == 14)
        {
            transform.eulerAngles = new Vector3(4.688f, 75.47501f, -96.13801f);
            ship.transform.position = shipCollider.transform.position;
        }
    }


    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        if(canMove)
        {
            transform.Rotate((new Vector3(10f * hor, 0.0f, 10.0f * ver) * Time.deltaTime), Space.Self);
        }

        if (ver !=0f || hor !=0f)
        {
            //Rotating(hor, ver);
        }
    }

    /*
    public void Rotating(float hor, float ver)
    {
        Vector3 targetDirection = new Vector3(hor, ver, 0f);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, transform.up);
        Quaternion newRotation = Quaternion.Lerp(ship.GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
        ship.GetComponent<Rigidbody>().MoveRotation(newRotation);
    }
    */
}
