using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    public PlayerScript player;
    public TongueScript tongue;
    public GameObject coinTracker;
    public GameObject turnipTracker;

    public Camera main;
    public GameObject[] cameras;
    public float[] cameraDurations;

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        main.enabled = false;
        coinTracker = GameObject.Find("CoinTracker");
        turnipTracker = GameObject.Find("TurnipTracker");
        coinTracker.SetActive(false);
        turnipTracker.SetActive(false);
        cameras[0].GetComponent<Camera>().enabled = true;
        cameras[1].GetComponent<Camera>().enabled = false;
        cameras[1].GetComponent<Animator>().enabled = false;
        cameras[2].GetComponent<Camera>().enabled = false;
        cameras[2].GetComponent<Animator>().enabled = false;
        StartCoroutine("CameraSwitcher");
    }

    public void Camera2()
    {
        cameras[0].GetComponent<Camera>().enabled = false;
        cameras[1].GetComponent<Camera>().enabled = true;
        cameras[1].GetComponent<Animator>().enabled = true;
        cameras[2].GetComponent<Camera>().enabled = false;
    }

    public void Camera3()
    {
        cameras[0].GetComponent<Camera>().enabled = false;
        cameras[1].GetComponent<Camera>().enabled = false;
        cameras[2].GetComponent<Camera>().enabled = true;
        cameras[2].GetComponent<Animator>().enabled = true;
    }

    public void GameOn()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
        }
        main.enabled = true;
        player.enabled = true;
        //tongue.enabled = true;
        player.Fade();
    }

    public IEnumerator CameraSwitcher()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        tongue = GameObject.Find("TongueBase").GetComponent<TongueScript>();
        player.enabled = false;
        //tongue.enabled = false;
        yield return new WaitForSeconds(cameraDurations[0]);
        Camera2();
        yield return new WaitForSeconds(cameraDurations[1]);
        Camera3();
        yield return new WaitForSeconds(cameraDurations[2]);
        coinTracker.SetActive(true);
        turnipTracker.SetActive(true);
        GameOn();
    }
}
