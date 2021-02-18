using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gasTankInfoScript : MonoBehaviour
{
    public GameObject player;

    public string[] dialogueLines;

    public string[] dialogueLines1;
    public string[] dialogueLines2;

    public Text     dialogueText;
    public string   currentLine;
    public string   currentText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueText.text = dialogueLines[0].ToString();
        StartCoroutine("CloseWindow");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine("CloseWindow");
        }
    }

    public IEnumerator CloseWindow()
    {
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerScript>().canMove = true;
        gameObject.SetActive(false);
    }
}
