using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public bool exitState = false;
    public SpaceShipScript spaceShipScript;

    // Start is called before the first frame update
    void Start()
    {
        spaceShipScript = GameObject.Find("SpaceShip").GetComponent<SpaceShipScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");

        if(v <= -0.1f && !exitState)
        {
            exitState = true;
        }

        else if(v >= 0.1f && exitState)
        {
            exitState = false;
        }

        if(Input.GetButtonDown("Jump") && exitState)
        {
            StartCoroutine("ExitLevel");
        }
    }

    public IEnumerator ExitLevel()
    {
        GameObject.Find("Player").GetComponent<PlayerScript>().paused = false;
        yield return new WaitForSeconds(0.5f);
        spaceShipScript.ChangeLevel();
    }
}
