using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    private Vector3 currentEulerAngles;

    public float speed = 5f;
    public float turnSmoothing = 5f;


    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    public GameObject ship;
    private Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        transform.Rotate((new Vector3(10f*hor, 0.0f, 10.0f*ver) * Time.deltaTime), Space.Self);

        if(ver !=0f || hor !=0f)
        {
            Rotating(hor, ver);
        }
    }

    public void Rotating(float hor, float ver)
    {
        Vector3 targetDirection = new Vector3(hor, ver, 0f);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, transform.up);
        Quaternion newRotation = Quaternion.Lerp(ship.GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
        ship.GetComponent<Rigidbody>().MoveRotation(newRotation);
    }
}
