using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MapMovement : MonoBehaviour
{
    public GameObject landingPrompt;
    public int levelToLoad;

    private void Start()
    {
        landingPrompt.SetActive(false);
    }

    private void Update()
    {
        if(landingPrompt.activeSelf && Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LandingPlatform")
        {
            landingPrompt.SetActive(true);
            landingPrompt.GetComponentInChildren<Text>().text = "Land to " + other.gameObject.GetComponent<LanderAreaScript>().planetName;
            levelToLoad = other.gameObject.GetComponent<LanderAreaScript>().levelNumber;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LandingPlatform")
        {
            landingPrompt.SetActive(false);
        }
    }
}
