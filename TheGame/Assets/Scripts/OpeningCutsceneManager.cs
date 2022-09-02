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

    public GameObject theTipu;

    public GameObject villain;

    public float timer;

    public GameObject fade;

    public AudioSource audioSource;
    public AudioClip[] music;

    // Start is called before the first frame update
    void Start()
    {
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
        theTipu.GetComponent<TipuToRescue>().eyes[0].SetActive(true);
        theTipu.GetComponent<TipuToRescue>().eyes[1].SetActive(false);
        theTipu.GetComponent<TipuToRescue>().small = false;
        yield return new WaitForSeconds(1f);
        theTipu.GetComponent<TipuToRescue>().Stop();
        yield return new WaitForSeconds(1f);
        theTipu.GetComponent<TipuToRescue>().Wave();
        theTipu.GetComponent<TipuToRescue>().eyes[0].SetActive(false);
        theTipu.GetComponent<TipuToRescue>().eyes[1].SetActive(true);
    }

    public IEnumerator LoadMenu()
    {
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("HomeLevel");
    }
}
