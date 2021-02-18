using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public GameObject dialogueIndicator;

    public enum NPCcharacter { Bird, Bear, Fish, Fox}
    public NPCcharacter character;

    public int dialogueNumber;

    public int npcNumber;
    public bool missionComplete = false;

    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueIndicator.SetActive(false);
    }

    public void SetDialogue()
    {
        if(!missionComplete)
        {
            switch (character)
            {
                case NPCcharacter.Bird:
                    dialogueNumber = 1;
                    break;

                case NPCcharacter.Bear:
                    dialogueNumber = 2;
                    break;

                case NPCcharacter.Fish:
                    dialogueNumber = 3;
                    break;

                case NPCcharacter.Fox:
                    dialogueNumber = 4;
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (character)
            {
                case NPCcharacter.Bird:
                    dialogueNumber = 5;
                    break;

                case NPCcharacter.Bear:
                    dialogueNumber = 6;
                    break;

                case NPCcharacter.Fish:
                    dialogueNumber = 7;
                    break;

                case NPCcharacter.Fox:
                    dialogueNumber = 8;
                    break;

                default:
                    break;
            }
        }


        dialogueManager.ChangeDialogue(dialogueNumber);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dialogueIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogueIndicator.SetActive(false);
        }
    }
}
