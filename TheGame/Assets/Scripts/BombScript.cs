using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float timeToExplode = 5f;
    public float timer;
    public GameObject explosionParticle;
    public CameraScript camera;
    public GameObject player;
    private Tongue tongue;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<CameraScript>();
        player = GameObject.Find("Player");
        tongue = GameObject.Find("TongueBase").GetComponent<Tongue>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeToExplode)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosionParticle, transform.position, transform.rotation);
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist < 25f)
        {
            //camera.SendMessage("StartScreenShake");
        }

        if(dist<7)
        {
            player.GetComponent<PlayerScript>().StartCoroutine("Damage");
        }
        tongue.carryingBomb = false;
        tongue.bomb = null;
        Destroy(gameObject);
    }
}
