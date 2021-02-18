using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballScript : MonoBehaviour
{
    public Vector3 scale = new Vector3(1f,1f,1f);
    public bool rolling = false;

    private Rigidbody myRB;
    private PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        if(transform.localPosition.y <=0f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 1f, transform.localPosition.z);
        }
    }

    void FixedUpdate()
    {
        if(myRB.velocity.x != 0f && rolling || myRB.velocity.z !=0f && rolling)
        {
            if(transform.localScale.x <= 1.6f)
            {
                scale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
                transform.localScale = scale;
                //myRB.angularDrag = 0f;
                //myRB.mass = 0.1f;
            }
            else
            {
                return;
            }

        }

        else
        {
            //myRB.angularDrag = 100f;
            //myRB.mass = 100f;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "SnowBall")
        {
            if(transform.localScale.x > 1.5f && other.gameObject.transform.localScale.x >=1.5f)
            {
                player.activeSnowball = null;
                player.speed = player.storedSpeed;
                transform.position = new Vector3(other.gameObject.transform.position.x,other.gameObject.transform.position.y+1.6f,other.gameObject.transform.position.z);
                myRB.useGravity = false;
                myRB.isKinematic = true;
            }
        }
    }
}
