using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGoldenTurnip : MonoBehaviour
{
    public int turnipNumber;
    public bool collected = false;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //collected = gm.goldenTurnipObjects[turnipNumber]
    }

    public CollectableGoldenTurnip(int myNumber, bool amIcollected)
    {
        myNumber        = turnipNumber;
        amIcollected    = collected;
    }
}
