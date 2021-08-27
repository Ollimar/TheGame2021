using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public GameObject dustParticle;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BreakableWall")
        {
            Instantiate(dustParticle, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Destroy(other.gameObject);  
        }
    }
}
