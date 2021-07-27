using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool caught = false;
    public bool eaten = false;

    public bool moving = false;

    public float timer;
    private Animator myAnim;

    public float speed;
    public float moveSpeed = 6f;

    public ParticleSystem puff;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        puff = GetComponentInChildren<ParticleSystem>();
        puff.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (timer > Random.Range(3f,10f))
        {
            moving = true;
        }

        if(caught)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
        }

        if(eaten)
        {
            puff.transform.parent = null;
            puff.Stop();
            moving = false;
            timer = 0f;
            transform.localScale -= new Vector3(10, 10, 10)*Time.deltaTime;
        }

        if(transform.localScale.x <= 0.1f)
        {
            Destroy(gameObject);
        }

        if(moving)
        {
            myAnim.SetBool("Walking",true);
            speed = moveSpeed;
            if(timer > Random.Range(6f,10f))
            {
                timer = 0f;
                moving = false;
                moveSpeed = -moveSpeed;
            }
        }
        else if(!moving)
        {
            myAnim.SetBool("Walking", false);
            speed = 0f;
        }
    }

    /*
    private void FixedUpdate()
    {

        Ray ray;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.right*2f, out hit) || Physics.Raycast(transform.position, Vector3.right * -2f, out hit))
        {
            timer = 0f;
            moving = false;
            speed = -speed;
        }
    }
    */
}
