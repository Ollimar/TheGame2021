using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObjectReturn : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;

    public bool canPull = true;
    public bool pull = false;

    public float transitionTime = 1f;

    public ParticleSystem[] effects;

    private AudioSource myAudio;
    public  AudioClip   openSound;
    public  AudioClip   tickSound;
    public bool soundPlayed = false;

    public bool pulled = false;
    public float returnTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        effects[0].Stop();
        effects[1].Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(pull && canPull)
        {
            if (!myAudio.isPlaying && !soundPlayed)
            {
                myAudio.PlayOneShot(openSound);
                soundPlayed = true;
            }
            effects[0].Play();
            effects[1].Play();
            transform.position = Vector3.Lerp(transform.position, endPosition.position, 10f * Time.deltaTime);
            StartCoroutine("Pull");
        }

        if(!pulled)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition.position, 10f * Time.deltaTime);
        }

    }

    public IEnumerator StartPull()
    {
        yield return new WaitForSeconds(transitionTime);
        pull = true;
    }

    public IEnumerator Pull()
    {
        gameObject.tag = "Untagged";
        GameObject.Find("TongueBase").GetComponent<Tongue>().attachedObject = null;
        pulled = true;
        yield return new WaitForSeconds(1f);
        pull = false;
        canPull = false;
        effects[0].Stop();
        effects[1].Stop();       
        yield return new WaitForSeconds(returnTime/2);
        if(!myAudio.isPlaying)
        {
            myAudio.clip = tickSound;
            myAudio.Play();
            myAudio.loop = true;
        }
        yield return new WaitForSeconds(returnTime / 2);
        myAudio.loop = false;
        myAudio.Stop();
        
        canPull = true;
        pulled = false;
        gameObject.tag = "PullObjectReturn";
    }
}
