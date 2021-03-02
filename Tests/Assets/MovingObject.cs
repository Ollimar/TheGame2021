using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float tspeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        transform.Translate(transform.forward*ver*tspeed*Time.deltaTime);
        transform.Rotate(Vector3.up * hor*tspeed * Time.deltaTime);
    }
}
