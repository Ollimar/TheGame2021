using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLevelObject : MonoBehaviour
{
    private GameManager gm;
    private PlayerScript player;

    public int objectNumber;

    public int object1Price = 30;

    public GameObject object1UI;

    public bool purchased = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        object1UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && object1UI.activeSelf && !purchased)
        {
            if(gm.coins > object1Price)
            {
                gm.missions[0] = true;
                gm.coins = gm.coins - object1Price;
                player.coinsCollected.text = gm.coins.ToString();
                purchased = true;
                object1UI.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                return;
            }
        }

        if (Input.GetButtonDown("Fire1") && object1UI.activeSelf)
        {
            object1UI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !purchased)
        {
            object1UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            object1UI.SetActive(false);
        }
    }
}
