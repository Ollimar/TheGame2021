using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerMapLevel : MonoBehaviour
{
    private AudioSource myAudio;
    public float fullVolume = 0.5f;
    public float TransitionTime = 0.05f;
    public bool musicOn = false;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(myAudio.volume <= fullVolume)
        {
            myAudio.volume = Mathf.Lerp(myAudio.volume, fullVolume, TransitionTime * Time.deltaTime);
        }
    }

    public void TurnipCollected()
    {
        myAudio.volume = 0f;
    }
}
