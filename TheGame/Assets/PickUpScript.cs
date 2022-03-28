using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpScript : MonoBehaviour
{
    public GameObject effect;
    public Text coinTracker;
    private AudioSource myAudio;
    private GameManager gm;

    public AudioClip collected;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinTracker = GameObject.Find("CoinTracker").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(gm.coins != null)
            {
                gm.coins += 1;
            }
            coinTracker.text = "X " + gm.coins;
            if(effect != null)
                Instantiate(effect, transform.position, transform.rotation);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            if(!myAudio.isPlaying && collected !=null)
            {
                myAudio.PlayOneShot(collected);
            }
            StartCoroutine("TimeToDisappear");
        }
    }

    public IEnumerator TimeToDisappear()
    {
        yield return new WaitForSeconds(1f);
        if(transform.parent!=null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
