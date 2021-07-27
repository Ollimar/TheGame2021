using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudInSpaceScript : MonoBehaviour
{

    public GameObject[] coins;
    public Transform[] spawners;
    public Transform holder;
    public GameObject spaceShip;
    public GameObject planet;
    private int dist = 1;
    private Vector3 shipDirection;

    // Start is called before the first frame update
    void Start()
    {
        spaceShip   = GameObject.Find("SpaceShip");
        planet      = GameObject.Find("Rotator");
    }

    // Update is called once per frame
    void Update()
    {
        //holder.eulerAngles = new Vector3(transform.eulerAngles.x, spaceShip.transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpaceShip")
        {
            shipDirection = spaceShip.transform.localPosition;
            StartCoroutine("SpawnCoins");
        }
    }

    public IEnumerator SpawnCoins()
    {
        for(int i=0; i<coins.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            dist += 2;
            Instantiate(coins[i], spawners[i].position, planet.transform.rotation);
        }

        Destroy(gameObject);
        
    }
}
