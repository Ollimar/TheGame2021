using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningCutsceneManager : MonoBehaviour
{
    public GameObject[] cameras;

    public GameObject cabin;

    public GameObject[] tipus;

    public Transform[] tipuStartPositions;

    public GameObject theTipu;

    public GameObject villain;

    //public GameObject clouds; 

    public float timer;

    public GameObject fade;

    public AudioSource audioSource;
    public AudioClip[] music;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = cameras[0].transform.position;
        Camera.main.transform.rotation = cameras[0].transform.rotation;
        //clouds.SetActive(false);
        audioSource.clip = music[0];
        audioSource.Play();
        StartCoroutine("Scene1");
        
        for (int i=0; i< tipus.Length;i++)
        {
            tipus[i].GetComponent<TipuToRescue>().tears[0].Stop();
            tipus[i].GetComponent<TipuToRescue>().tears[1].Stop();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(Input.GetButtonDown("Jump"))
        {
            StartCoroutine("LoadMenu");
        }
    }

    public IEnumerator Scene1()
    {
        yield return new WaitForSeconds(5f);
        cabin.GetComponent<Animator>().SetBool("DoorOpen", true);
        audioSource.clip = music[1];
        audioSource.Play();
        tipus[0].GetComponent<TipuToRescue>().Running();       
        tipus[0].GetComponent<TipuToRescue>().small = false;       
        yield return new WaitForSeconds(2f);
        tipus[1].GetComponent<TipuToRescue>().Running();
        tipus[1].GetComponent<TipuToRescue>().small = false;
        yield return new WaitForSeconds(2f);
        tipus[2].GetComponent<TipuToRescue>().Running();
        tipus[2].GetComponent<TipuToRescue>().small = false;
        yield return new WaitForSeconds(2f);
        tipus[3].GetComponent<TipuToRescue>().Running();
        tipus[3].GetComponent<TipuToRescue>().small = false;
        yield return new WaitForSeconds(2f);
        tipus[4].GetComponent<TipuToRescue>().Running();
        tipus[4].GetComponent<TipuToRescue>().small = false;
        yield return new WaitForSeconds(2f);
        tipus[5].GetComponent<TipuToRescue>().Running();
        tipus[5].GetComponent<TipuToRescue>().small = false;
        yield return new WaitForSeconds(3f);
        theTipu.GetComponent<TipuToRescue>().Running();
        theTipu.GetComponent<TipuToRescue>().tear.SetActive(false);
        theTipu.GetComponent<TipuToRescue>().eyes[0].SetActive(true);
        theTipu.GetComponent<TipuToRescue>().eyes[1].SetActive(false);
        theTipu.GetComponent<TipuToRescue>().eyes[2].SetActive(false);
        theTipu.GetComponent<TipuToRescue>().small = false;
        yield return new WaitForSeconds(1f);
        theTipu.GetComponent<TipuToRescue>().Stop();
        yield return new WaitForSeconds(1f);
        theTipu.GetComponent<TipuToRescue>().Wave();
        theTipu.GetComponent<TipuToRescue>().eyes[0].SetActive(false);
        theTipu.GetComponent<TipuToRescue>().eyes[1].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("Scene2");
    }

    public IEnumerator Scene2()
    {
        Camera.main.transform.position = cameras[1].transform.position;
        Camera.main.transform.rotation = cameras[1].transform.rotation;
        yield return new WaitForSeconds(1f);
        tipus[0].transform.position = tipuStartPositions[1].position;
        tipus[0].transform.rotation = tipuStartPositions[1].rotation;
        yield return new WaitForSeconds(1f);
        tipus[1].transform.position = tipuStartPositions[1].position;       
        tipus[1].transform.rotation = tipuStartPositions[1].rotation;
        yield return new WaitForSeconds(0.5f);
        tipus[1].GetComponent<Animator>().SetTrigger("ShortFly");
        yield return new WaitForSeconds(0.5f);
        tipus[2].transform.position = tipuStartPositions[1].position;
        tipus[2].transform.rotation = tipuStartPositions[1].rotation;
        yield return new WaitForSeconds(0.5f);
        tipus[2].GetComponent<Animator>().SetTrigger("Somersault");
        yield return new WaitForSeconds(0.5f);
        tipus[3].transform.position = tipuStartPositions[1].position;
        tipus[3].transform.rotation = tipuStartPositions[1].rotation;
        yield return new WaitForSeconds(1f);
        tipus[4].transform.position = tipuStartPositions[1].position;
        tipus[4].transform.rotation = tipuStartPositions[1].rotation;
        yield return new WaitForSeconds(0.25f);
        tipus[4].GetComponent<Animator>().SetTrigger("ShortFly");
        yield return new WaitForSeconds(0.75f);
        tipus[5].transform.position = tipuStartPositions[1].position;
        tipus[5].transform.rotation = tipuStartPositions[1].rotation;
        tipus[5].GetComponent<Animator>().SetTrigger("Somersault");
        yield return new WaitForSeconds(0.75f);
        tipus[5].GetComponent<Animator>().SetTrigger("Somersault");
        yield return new WaitForSeconds(2f);
        StartCoroutine("Scene3");
    }

    public IEnumerator Scene3()
    {
        theTipu.SetActive(false);
        Camera.main.transform.position = cameras[2].transform.position;
        Camera.main.transform.rotation = cameras[2].transform.rotation;
        yield return new WaitForSeconds(1f);
        StartCoroutine("TipuRunning");
        //clouds.SetActive(true);
    }

    public IEnumerator TipuRunning()
    {       
        theTipu.SetActive(true);
        theTipu.transform.position = tipuStartPositions[2].position;
        Camera.main.GetComponent<OpeningCutSceneCameraScript>().direction = new Vector3(0f, 0f, 1f);
        Camera.main.GetComponent<OpeningCutSceneCameraScript>().speed = -1.1f;
        Camera.main.GetComponent<OpeningCutSceneCameraScript>().MovingCamera();
        theTipu.GetComponent<TipuToRescue>().Running();
        theTipu.GetComponent<TipuToRescue>().tear.SetActive(true);
        //theTipu.GetComponent<TipuToRescue>().sweat.SetActive(true);
        theTipu.GetComponent<TipuToRescue>().eyes[1].SetActive(false);
        theTipu.GetComponent<TipuToRescue>().eyes[0].SetActive(false);
        theTipu.GetComponent<TipuToRescue>().eyes[2].SetActive(true);
        Camera.main.transform.position = cameras[3].transform.position;
        Camera.main.transform.rotation = cameras[3].transform.rotation;
        Camera.main.transform.parent = theTipu.transform;
        yield return new WaitForSeconds(5f);
        theTipu.GetComponent<TipuToRescue>().Stop();
        theTipu.GetComponent<TipuToRescue>().eyes[2].SetActive(false);
        theTipu.GetComponent<TipuToRescue>().eyes[0].SetActive(true);
        theTipu.GetComponent<TipuToRescue>().sweat.SetActive(false);
        theTipu.GetComponent<TipuToRescue>().tear.SetActive(false);
        Camera.main.GetComponent<OpeningCutSceneCameraScript>().speed = -5f;
        yield return new WaitForSeconds(3f);
        StartCoroutine("LoadMenu");
    }

    public IEnumerator FinalScene()
    {
        yield return new WaitForSeconds(1f);
        LoadMenu();
    }

    public IEnumerator LoadMenu()
    {
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("HomeLevel");
    }
}
