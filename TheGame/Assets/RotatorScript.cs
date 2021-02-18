using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    public float speed = 5f;
    public float turnSmoothing = 5f;

    public GameObject ship;
    private Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = ship.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        transform.Rotate(ver*speed*Time.deltaTime,hor*speed*Time.deltaTime,0f,0f);

        if (hor != 0f || ver != 0f)
        {
            Rotating(hor, ver);
        }

    }

    public void Rotating(float hor, float ver)
    {
        Vector3 targetDirection = new Vector3(hor, 0f, ver);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(myRB.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        myRB.MoveRotation(newRotation);
    }
}
