using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{

    private GameManager gm;

    public int object1Price = 30;
    public int object2Price = 30;
    public int object3Price = 30;

    public GameObject object1Info;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        object1Info.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Object1()
    {
        if(gm.coins > object1Price)
        {
            gm.coins = gm.coins - object1Price;
        }
        else
        {
            return;
        }
    }
}
