using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool active = true;

    public Vector3 movement;
    public float speed = 5f;

    public float timer;
    public float changeTime = 3f;

    private Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(active)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }

        if (timer > changeTime)
        {
            speed = -speed;
            timer = 0f;
        }
    }

    /*
    void FixedUpdate()
    {
        myRB.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);

        if(timer > changeTime)
        {
            speed = -speed;
            timer = 0f;
        }
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!active)
            {
                active = true;
            }
        }
    }
}
