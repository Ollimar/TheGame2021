using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public PlayerScript player;
    public GameObject dialogueWindow;
    public Text dialogueText;

    public string[] dialogueLines;

    public string[] dialogueLines1;
    public string[] dialogueLines2;
    public string[] dialogueLines3;
    public string[] dialogueLines4;
    public string[] dialogueLines5;
    public string[] dialogueLines6;
    public string[] dialogueLines7;
    public string[] dialogueLines8;
    public string[] dialogueLines9;

    public string currentLine;

    public string currentText;

    public int lineNumber;
    public int dialogueNumber;
    public bool lineFull = true;

    public CameraScript cameraScript;

    // Start is called before the first frame update
    void Start()
    {
        dialogueWindow = GameObject.Find("DialogueBoxTest");
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        dialogueWindow.SetActive(false);
        dialogueText.text = currentLine;
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && dialogueWindow.activeSelf)
        {
            ChangeLine();
        }
    }

    public void ChangeDialogue(int diNum)
    {
        lineNumber = 0;
        StartCoroutine("ShowText");


        if (diNum == 1)
        {
            dialogueLines = dialogueLines1;
        }

        else if(diNum == 2)
        {
            dialogueLines = dialogueLines2;
        }

        else if (diNum == 3)
        {
            dialogueLines = dialogueLines3;
        }

        else if (diNum == 4)
        {
            dialogueLines = dialogueLines4;
        }

        else if (diNum == 5)
        {
            dialogueLines = dialogueLines5;
        }

        else if (diNum == 6)
        {
            dialogueLines = dialogueLines6;
        }

        else if (diNum == 7)
        {
            dialogueLines = dialogueLines7;
        }

        else if (diNum == 8)
        {
            dialogueLines = dialogueLines8;
        }

        else if (diNum == 9)
        {
            dialogueLines = dialogueLines9;
        }


        currentLine = dialogueLines[lineNumber];
        dialogueText.text = currentLine;
    }

    public void ChangeLine()
    {
        lineNumber++;
        lineFull = false;

        if (lineNumber > dialogueLines.Length-1)
        {
            cameraScript.ReturnCamera();
            dialogueWindow.SetActive(false);
            player.canMove = true;
            //lineNumber = 0;
        }

        else
        {
            currentLine = dialogueLines[lineNumber];
            StartCoroutine("ShowText");
            lineFull = true;
        }
    }

    public void CloseDialogue()
    {
        cameraScript.ReturnCamera();
        lineNumber = 0;
        dialogueWindow.SetActive(false);
    }

    public void ReturnLevel()
    {
        dialogueWindow = GameObject.Find("DialogueBoxTest");
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        dialogueWindow.SetActive(false);
        dialogueText.text = currentLine;
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    public IEnumerator ShowText()
    {
        for (int i = 0; i < currentLine.Length; i++)
        {
            currentText = currentLine.Substring(0, i+1);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
