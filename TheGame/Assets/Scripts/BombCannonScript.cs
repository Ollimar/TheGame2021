using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCannonScript : MonoBehaviour
{

    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot",1f,5f);
    }


    public void Shoot()
    {
        GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation);
        newBomb.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
    }
}
