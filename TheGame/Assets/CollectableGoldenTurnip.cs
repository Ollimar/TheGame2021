using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableGoldenTurnip : MonoBehaviour
{
    public int turnipNumber;
    public int correctlevel;
    public bool collected = false;

    public GameObject childObject;

    public Material goldMaterial;
    public Material glassMaterial;

    private GameManager gm;

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(true);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(gm.levelInfo.levelNumber != correctlevel)
        {
            gameObject.SetActive(false);
        }

        else if(gm.levelInfo.levelNumber == correctlevel)
        {
            gameObject.SetActive(true);

            if (!collected)
            {
                childObject.GetComponent<Renderer>().material = goldMaterial;
            }

            if (collected)
            {
                childObject.GetComponent<Renderer>().material = glassMaterial;
            }
        }
    }

    // Called from gamemanager every time level changes 
    public void CheckTurnips()
    {
        gameObject.SetActive(true);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gm.levelInfo.levelNumber != correctlevel)
        {
            gameObject.SetActive(false);
        }

        else if (gm.levelInfo.levelNumber == correctlevel)
        {
            gameObject.SetActive(true);

            if (!collected)
            {
                childObject.GetComponent<Renderer>().material = goldMaterial;
            }

            if (collected)
            {
                childObject.GetComponent<Renderer>().material = glassMaterial;
            }
        }
    }


}
