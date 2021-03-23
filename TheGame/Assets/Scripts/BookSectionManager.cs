using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookSectionManager : MonoBehaviour
{

    public GameObject[] levels;
    public int currentLevel;
    public GameObject page;
    public GameObject player;
    public GameObject doorIn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for(int i=0; i<levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levels[currentLevel].SetActive(true);
        page.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator NextPage()
    {
        levels[currentLevel].SetActive(false);
        player.SetActive(false);
        currentLevel++;
        page.SetActive(true);
        page.GetComponent<Animator>().SetTrigger("NextPage");
        yield return new WaitForSeconds(3f);
        page.SetActive(false);
        page.GetComponent<Animator>().SetTrigger("FirstPage");
        levels[currentLevel].SetActive(true);
        doorIn = GameObject.Find("DoorIn");
        player.SetActive(true);
        player.transform.position = doorIn.transform.position;
    }

    public IEnumerator LastPage()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("HomeUpStairs");
    }
}
