using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpScript : MonoBehaviour
{
    public GameObject effect;
    public Text coinTracker;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinTracker = GameObject.Find("CoinTracker").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gm.coins += 1;
            coinTracker.text = "x" + gm.coins;
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
