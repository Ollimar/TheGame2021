using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerSword : MonoBehaviour
{

    public bool canDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<SideScrollerEnemy>().Damage();
        }
    }
}
