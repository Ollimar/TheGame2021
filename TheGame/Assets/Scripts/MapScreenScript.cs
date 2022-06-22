using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScreenScript : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] levels;
    private MapMovement mapMovement;

    public bool[] levelUnlocked;

    public GameObject newLevelEffect;

    // Start is called before the first frame update

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mapMovement = GameObject.FindGameObjectWithTag("SpaceShip").GetComponent<MapMovement>();

        for (int i=0; i<levels.Length; i++)
        {
            levels[i].SetActive(false);
            levels[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int i = 0; i < gm.levelUnlocked.Length; i++)
        {
            levelUnlocked[i] = gm.levelUnlocked[i];
            levels[i].transform.GetChild(0).gameObject.SetActive(true);
        }
        
        levels[0].SetActive(true);

        if(gm.goldenTurnips >= 1)
        {
            levels[1].SetActive(true);
            if(!levelUnlocked[1])
            {
                levels[1].transform.GetChild(0).gameObject.SetActive(false);
                StartCoroutine(ShowNewLevel(1));
            }
        }

        if (gm.goldenTurnips >= 6)
        {
            levels[2].SetActive(true);
            if (!levelUnlocked[2])
            {
                levels[2].transform.GetChild(0).gameObject.SetActive(false);
                StartCoroutine(ShowNewLevel(2));
            }
        }

        if (gm.goldenTurnips >= 10 && levelUnlocked[2])
        {
            levels[3].SetActive(true);
            if (!levelUnlocked[3])
            {
                levels[3].transform.GetChild(0).gameObject.SetActive(false);
                StartCoroutine(ShowNewLevel(3));
            }
        }

        if (gm.goldenTurnips >= 15 && levelUnlocked[3])
        {
            levels[4].SetActive(true);
            if (!levelUnlocked[4])
            {
                levels[4].transform.GetChild(0).gameObject.SetActive(false);
                StartCoroutine(ShowNewLevel(4));
            }
        }

        if (gm.goldenTurnips >= 22 && levelUnlocked[4])
        {
            levels[5].SetActive(true);
            if (!levelUnlocked[5])
            {
                levels[5].transform.GetChild(0).gameObject.SetActive(false);
                StartCoroutine(ShowNewLevel(5));
            }
        }
    }


    public IEnumerator ShowNewLevel(int levelNumber)
    {
        mapMovement.canEnterLevel = false;
        yield return new WaitForSeconds(2f);
        mapMovement.cameraTarget = levels[levelNumber].transform.GetComponentInChildren<LanderAreaScript>().cameraTarget;
        mapMovement.playerPos = GameObject.Find("SpaceShip").transform;
        mapMovement.zoomToLevel = true;
        mapMovement.newLevelUnlocked = true;
        yield return new WaitForSeconds(1f);
        Instantiate(newLevelEffect, levels[levelNumber].transform.position, levels[levelNumber].transform.rotation);
        levels[levelNumber].transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        mapMovement.zoomToLevel = false;
        mapMovement.newLevelUnlocked = false;
        gm.levelUnlocked[levelNumber] = true;
        mapMovement.canEnterLevel = true;

        for (int i = 0; i < gm.levelUnlocked.Length; i++)
        {
            levelUnlocked[i] = gm.levelUnlocked[i];
            levels[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
