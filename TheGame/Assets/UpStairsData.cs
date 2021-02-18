using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairsData : MonoBehaviour
{
    private GameManager gm;

    public GameObject[] decorationItems;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<decorationItems.Length; i++)
        {
            decorationItems[i].SetActive(false);
        }

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(gm.missions[0])
        {
            decorationItems[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
