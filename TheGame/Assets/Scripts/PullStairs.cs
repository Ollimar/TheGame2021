using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullStairs : MonoBehaviour
{

    private Animator myAnim;

    public bool pull;
    public bool canPull = true;

    public float transitionTime = 1f;

    public GameObject coin;

    public Transform[] coinSpawners;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pull && canPull)
        {
            //StartCoroutine("Pull");
        }
    }

    public void Open()
    {
        myAnim.SetTrigger("Open");
        
    }

    public IEnumerator StartPull()
    {
        myAnim.SetTrigger("Open");
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine("SpawnCoin");
        pull = true;
    }

    public IEnumerator Pull()
    {
        gameObject.tag = "Untagged";
        GameObject.Find("TongueBase").GetComponent<Tongue>().attachedObject = null;
        yield return new WaitForSeconds(1f);
        pull = false;
        canPull = false;
        Open();
        SpawnCoin();
        //effects[0].Stop();
        //effects[1].Stop();
        //Destroy(this);
    }

    public IEnumerator SpawnCoin()
    {
        for(int i=0; i<coinSpawners.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject newCoin = Instantiate(coin, coinSpawners[i].transform.position, coinSpawners[i].transform.rotation);
            newCoin.GetComponent<Rigidbody>().AddForce(Vector3.up * 400f);
        }

    }
}
