using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;

    public bool canMove = true;
    public float walkSpeed = 15f;
    public float jumpSpeed = 50f;

    public bool movingRight = true;

    public Transform swordHolder;
    public Animator swordAnimator;

    public bool canJump = false;
    public Transform collisionPoint;
    public float collisonRadius = 1f;
    public LayerMask layerMask;

    public GameObject book;
    private BookSectionManager bm;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        bm = GameObject.Find("BookSectionManager").GetComponent<BookSectionManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();     
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if (Input.GetButtonDown("Jump") && canJump)
            {
                myRB.AddForce(Vector2.up * jumpSpeed);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                swordAnimator.SetTrigger("Swing");
            }
        }
    }

    public void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");

        if(canMove)
        {
            myRB.velocity = new Vector2(hor * walkSpeed, myRB.velocity.y);
        }

        if (hor >= 0.1f && !movingRight)
        {
            Flip();
        }

        else if (hor <= -0.1f && movingRight)
        {
            Flip();
        }

        if(Physics2D.OverlapCircle(collisionPoint.position, collisonRadius,layerMask))
        {
            canJump = true;
        }
        else 
        {
            canJump = false;
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        movingRight = !movingRight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "LevelGoal")
        {
            gm.startFromBed = true;
            SceneManager.LoadScene("HomeUpStairs");
        }

        if (other.gameObject.tag == "NextPage")
        {
            bm.StartCoroutine("NextPage");
        }

        if (other.gameObject.tag == "LastPage")
        {
            bm.StartCoroutine("LastPage");
        }
    }
}
