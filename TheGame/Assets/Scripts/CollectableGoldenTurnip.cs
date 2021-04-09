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

    public GameObject[] goldenTurnipSpots;

    private ParticleSystem myParticle;
    private GameManager gm;

    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckTurnips;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        myParticle = GetComponent<ParticleSystem>();
        if (!collected)
        {
            childObject.GetComponent<Renderer>().material = goldMaterial;
        }

        if (collected)
        {
            childObject.GetComponent<Renderer>().material = glassMaterial;
            myParticle.Stop();
        }
    }

    // Called from gamemanager every time level changes 
 
    public void CheckTurnips(Scene scene, LoadSceneMode mode)
    {
        goldenTurnipSpots = GameObject.FindGameObjectsWithTag("Golden Turnip Spot");

        for(int i=0; i<goldenTurnipSpots.Length;i++)
        {
            if(turnipNumber == goldenTurnipSpots[i].GetComponent<GoldenTurnipSpotScript>().turnipNumber)
            {
                transform.position = goldenTurnipSpots[i].transform.position;
            }
        }

        if (!collected)
        {
            childObject.GetComponent<Renderer>().material = goldMaterial;
        }

        if (collected)
        {
            childObject.GetComponent<Renderer>().material = glassMaterial;
            myParticle.Stop();
        }
    }  
}
