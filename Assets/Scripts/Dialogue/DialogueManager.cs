using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogueTxt;
    public GameObject dialogueBox;
    public bool inDialogue = false;
    public bool spawnEvidence;
    public Movement playerMovement;
    public CameraMove cameraMove;
    public GameObject interactUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
    }
    private void Update()
    {
        if (inDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (sentences.Count > 0)
                {
                    Debug.Log("NEXT SENTENCE");
                    DisplayNextSentence();
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Dialogue started");
        sentences.Clear();
        playerMovement.GetComponent<Movement>().enabled = false;
        cameraMove.GetComponent<CameraMove>().enabled = false;
        //interactUI.SetActive(false);

        foreach (string sentence in dialogue.sentences)
        {
            Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        dialogueBox.SetActive(true);
        nameTxt.text = dialogue.name;
        inDialogue = true;
    }

    public void DisplayNextSentence()
    {
        string sentence = sentences.Dequeue();
        dialogueTxt.text = sentence;
    }

    public void EndDialogue()
    {
        playerMovement.GetComponent<Movement>().enabled = true;
        cameraMove.GetComponent<CameraMove>().enabled = true;
        //interactUI.SetActive(true);
        spawnEvidence = true;
        inDialogue = false;
        dialogueBox.SetActive(false);
        Debug.Log("End of conversation.");
    }
}

