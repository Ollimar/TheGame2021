using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerEnemy : MonoBehaviour
{
    private Rigidbody2D myRB;
    public bool facingRight = false;

    public bool moving = true;
    public float speed = 5f;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if(moving)
        {
            myRB.velocity = new Vector2(speed, myRB.velocity.y);
        }
        else
        {
            myRB.velocity = new Vector2(myRB.velocity.x, myRB.velocity.y);
        }
    }

    public void Damage()
    {
        moving = false;
        print("hit");
        myRB.AddForce(new Vector2(500f, 100f));
        StartCoroutine("Recoil");
    }

    public IEnumerator Recoil()
    {
        yield return new WaitForSeconds(2f);
        moving = true;
    }
}
