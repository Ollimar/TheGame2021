using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCannonScript : MonoBehaviour
{

    public GameObject bomb;
    public bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot",1f,5f);
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canShoot = true;
        }
    }

    public void Shoot()
    {
        if(canShoot)
        {
            GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation);
            newBomb.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
        }
    }
}
