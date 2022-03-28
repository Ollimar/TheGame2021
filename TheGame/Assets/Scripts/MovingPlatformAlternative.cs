using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAlternative : MonoBehaviour
{
    public bool active = false;
    public float speed = 5;
    public Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            transform.Translate(movementDirection * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!active)
            {
                active = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bumper")
        {
            speed = -speed;
        }
    }
}
