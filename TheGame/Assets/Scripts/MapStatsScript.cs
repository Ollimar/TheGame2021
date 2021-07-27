using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 This script manages the coin and turnip counter. Normally this is handled inside player script, but it has to be disabled in map mode
 */

public class MapStatsScript : MonoBehaviour
{
    public Text turnipCounter;
    public Text coinCounter;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        turnipCounter.text = "X " + gm.goldenTurnips.ToString();
        coinCounter.text = "X " + gm.coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
