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
        }

        for(int i = 0; i < gm.levelUnlocked.Length; i++)
        {
            levelUnlocked[i] = gm.levelUnlocked[i];
        }

        levels[0].SetActive(true);

        if(gm.goldenTurnips >= 1)
        {
            levels[1].SetActive(true);
            levels[1].GetComponentInChildren<Renderer>().enabled = false;
            //levels[2].SetActive(true);
            if(!levelUnlocked[1])
            {
                StartCoroutine(ShowNewLevel(1));
            }

        }

        if (gm.goldenTurnips >= 10)
        {
            levels[3].SetActive(true);
        }

        if (gm.goldenTurnips >= 12)
        {
            levels[4].SetActive(true);
        }

        if (gm.goldenTurnips >= 15)
        {
            levels[4].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowNewLevel(int levelNumber)
    {
        yield return new WaitForSeconds(2f);
        mapMovement.cameraTarget = levels[levelNumber].transform.GetComponentInChildren<LanderAreaScript>().cameraTarget;
        mapMovement.playerPos = GameObject.Find("SpaceShip").transform;
        mapMovement.zoomToLevel = true;
        mapMovement.newLevelUnlocked = true;
        yield return new WaitForSeconds(1f);
        Instantiate(newLevelEffect, levels[1].transform.position, levels[1].transform.rotation);
        levels[1].GetComponentInChildren<Renderer>().enabled = true;
        yield return new WaitForSeconds(3f);
        mapMovement.zoomToLevel = false;
        mapMovement.newLevelUnlocked = false;
        gm.levelUnlocked[levelNumber] = true;
    }
}
