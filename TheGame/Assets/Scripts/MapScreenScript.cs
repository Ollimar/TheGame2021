using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScreenScript : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] levels;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();       

        if(gm.goldenTurnips >= 5)
        {
            levels[2].SetActive(true);
        }

        if (gm.goldenTurnips >= 10)
        {
            levels[3].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
