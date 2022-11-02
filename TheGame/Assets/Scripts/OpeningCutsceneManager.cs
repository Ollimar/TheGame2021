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
    public GameObject villainHand;
    public GameObject cage;

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
        villain.SetActive(false);
        villainHand.GetComponent<ParticleSystem>().Stop();
        cage.SetActive(false);
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
        villain.SetActive(true);
        tipus[0].transform.position = tipuStartPositions[3].position;
        tipus[0].transform.rotation = tipuStartPositions[3].rotation;
        tipus[0].GetComponent<Animator>().SetTrigger("Rescued");
        tipus[0].GetComponent<Animator>().SetBool("Running", false);
        tipus[0].GetComponent<TipuToRescue>().speed = 0f;
        tipus[0].GetComponent<TipuToRescue>().puff.SetActive(false);

        tipus[1].transform.position = tipuStartPositions[4].position;
        tipus[1].transform.rotation = tipuStartPositions[4].rotation;
        tipus[1].GetComponent<Animator>().SetTrigger("Rescued");
        tipus[1].GetComponent<Animator>().SetBool("Running", false);
        tipus[1].GetComponent<TipuToRescue>().speed = 0f;
        tipus[1].GetComponent<TipuToRescue>().puff.SetActive(false);

        tipus[2].transform.position = tipuStartPositions[5].position;
        tipus[2].transform.rotation = tipuStartPositions[5].rotation;
        tipus[2].GetComponent<Animator>().SetTrigger("Rescued");
        tipus[2].GetComponent<Animator>().SetBool("Running", false);
        tipus[2].GetComponent<TipuToRescue>().speed = 0f;
        tipus[2].GetComponent<TipuToRescue>().puff.SetActive(false);

        tipus[3].transform.position = tipuStartPositions[6].position;
        tipus[3].transform.rotation = tipuStartPositions[6].rotation;
        tipus[3].GetComponent<Animator>().SetTrigger("Rescued");
        tipus[3].GetComponent<Animator>().SetBool("Running", false);
        tipus[3].GetComponent<TipuToRescue>().speed = 0f;
        tipus[3].GetComponent<TipuToRescue>().puff.SetActive(false);

        tipus[4].transform.position = tipuStartPositions[7].position;
        tipus[4].transform.rotation = tipuStartPositions[7].rotation;
        tipus[4].GetComponent<Animator>().SetTrigger("Rescued");
        tipus[4].GetComponent<Animator>().SetBool("Running", false);
        tipus[4].GetComponent<TipuToRescue>().speed = 0f;
        tipus[4].GetComponent<TipuToRescue>().puff.SetActive(false);

        tipus[5].transform.position = tipuStartPositions[8].position;
        tipus[5].transform.rotation = tipuStartPositions[8].rotation;
        tipus[5].GetComponent<Animator>().SetTrigger("Rescued");
        tipus[5].GetComponent<Animator>().SetBool("Running", false);
        tipus[5].GetComponent<TipuToRescue>().speed = 0f;
        tipus[5].GetComponent<TipuToRescue>().puff.SetActive(false);

        Camera.main.GetComponent<OpeningCutSceneCameraScript>().MovingCamera();
        Camera.main.transform.position = cameras[2].transform.position;
        Camera.main.transform.rotation = cameras[2].transform.rotation;
        yield return new WaitForSeconds(5f);
        StartCoroutine("Scene4");
        //clouds.SetActive(true);
    }

    public IEnumerator Scene4()
    {
        theTipu.SetActive(false);
        villain.SetActive(true);

        tipus[0].GetComponent<Animator>().SetTrigger("Turn");
        tipus[1].GetComponent<Animator>().SetTrigger("Turn");
        tipus[2].GetComponent<Animator>().SetTrigger("Turn");
        tipus[3].GetComponent<Animator>().SetTrigger("Turn");
        tipus[4].GetComponent<Animator>().SetTrigger("Turn");
        tipus[5].GetComponent<Animator>().SetTrigger("Turn");

        Camera.main.transform.position = cameras[4].transform.position;
        Camera.main.transform.rotation = cameras[4].transform.rotation;

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < tipus.Length; i++)
        {
            tipus[i].GetComponent<TipuToRescue>().SadFace();
            tipus[i].GetComponent<Animator>().SetTrigger("Shiver");
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine("Scene5");
        //clouds.SetActive(true);
    }

    public IEnumerator Scene5()
    {
        Camera.main.transform.position = cameras[2].transform.position;
        Camera.main.transform.rotation = cameras[2].transform.rotation;

        theTipu.SetActive(false);
        villain.SetActive(true);

        for (int i = 0; i < tipus.Length; i++)
        {
            tipus[i].GetComponent<Animator>().SetBool("Running", true);
            tipus[i].GetComponent<TipuToRescue>().speed = 5f;
        }

        yield return new WaitForSeconds(1f);

        villainHand.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        villainHand.GetComponent<ParticleSystem>().Stop();
        cage.SetActive(true);

        for(int i=0; i<tipus.Length;i++)
        {
            tipus[i].GetComponent<Rigidbody>().useGravity = false;
            tipus[i].GetComponent<Rigidbody>().isKinematic = true;
            tipus[i].GetComponent<Animator>().SetBool("Running", false);
            tipus[i].GetComponent<Animator>().SetTrigger("Caught");
            tipus[i].GetComponent<TipuToRescue>().speed = 0f;
        }

        tipus[0].transform.parent = cage.transform;
        tipus[0].transform.position = new Vector3(cage.transform.position.x, cage.transform.position.y, cage.transform.position.z);

        tipus[1].transform.parent = cage.transform;
        tipus[1].transform.position = new Vector3(cage.transform.position.x+1f, cage.transform.position.y, cage.transform.position.z);

        tipus[2].transform.parent = cage.transform;
        tipus[2].transform.position = new Vector3(cage.transform.position.x - 1f, cage.transform.position.y, cage.transform.position.z);

        tipus[3].transform.parent = cage.transform;
        tipus[3].transform.position = new Vector3(cage.transform.position.x, cage.transform.position.y, cage.transform.position.z - 1f);

        tipus[4].transform.parent = cage.transform;
        tipus[4].transform.position = new Vector3(cage.transform.position.x+1f, cage.transform.position.y, cage.transform.position.z - 1f);

        tipus[5].transform.parent = cage.transform;
        tipus[5].transform.position = new Vector3(cage.transform.position.x - 1f, cage.transform.position.y, cage.transform.position.z - 1f);

        yield return new WaitForSeconds(5.5f);
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
        yield return new WaitForSeconds(1f);
        StartCoroutine("FinalScene");
    }

    public IEnumerator FinalScene()
    {
        Camera.main.GetComponent<OpeningCutSceneCameraScript>().speed = 0f;
        Camera.main.transform.position = cameras[5].transform.position;
        Camera.main.transform.rotation = cameras[5].transform.rotation;
        yield return new WaitForSeconds(0.5f);
        theTipu.GetComponent<TipuToRescue>().GetComponent<Animator>().SetTrigger("Sad");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("LoadMenu");
    }

    public IEnumerator LoadMenu()
    {
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("HomeLevel");
    }
}
