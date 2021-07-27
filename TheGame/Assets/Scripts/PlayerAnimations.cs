using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private AudioSource myAudio;
    public  AudioClip   stepSound;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Step()
    {
        myAudio.pitch = Random.Range(0.8f, 1.2f);
        myAudio.PlayOneShot(stepSound);
    }
}
