using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public int levelNumber;

    public bool levelBeaten = false;

    private GameManager gm;

    public GameObject birdCage;
    public GameObject tipuToRescue;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(levelNumber == 1 && gm.level1Beaten)
        {
            birdCage.SetActive(false);
            tipuToRescue.SetActive(false);
        }

        if (levelNumber == 2 && gm.level2Beaten)
        {
            birdCage.SetActive(false);
            tipuToRescue.SetActive(false);
        }

        if (levelNumber == 3 && gm.level3Beaten)
        {
            birdCage.SetActive(false);
            tipuToRescue.SetActive(false);
        }

        if (levelNumber == 4 && gm.level4Beaten)
        {
            birdCage.SetActive(false);
            tipuToRescue.SetActive(false);
        }

        if (levelNumber == 5 && gm.level5Beaten)
        {
            birdCage.SetActive(false);
            tipuToRescue.SetActive(false);
        }

        if (levelNumber == 6 && gm.level6Beaten)
        {
            birdCage.SetActive(false);
            tipuToRescue.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelBeaten()
    {
        if(levelNumber == 1)
        {
            gm.level1Beaten = true;
        }
        if (levelNumber == 2)
        {
            gm.level2Beaten = true;
        }
        if (levelNumber == 3)
        {
            gm.level3Beaten = true;
        }
        if (levelNumber == 4)
        {
            gm.level4Beaten = true;
        }
        if (levelNumber == 4)
        {
            gm.level4Beaten = true;
        }
        if (levelNumber == 5)
        {
            gm.level5Beaten = true;
        }
    }
}
