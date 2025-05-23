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
    public Movement playerMovement;
    public CameraMove cameraMove;
    public Animator playerAnimator;

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
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (sentences.Count > 0)
                {
                    //Debug.Log("NEXT SENTENCE");
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
        sentences.Clear();
        playerMovement.GetComponent<Movement>().enabled = false;
        cameraMove.GetComponent<CameraMove>().enabled = false;
        playerAnimator.GetComponent<Animator>().enabled = false;


        foreach (string sentence in dialogue.sentences)
        {
            //Debug.Log(sentence);
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
        playerAnimator.GetComponent<Animator>().enabled = true;
        inDialogue = false;
        dialogueBox.SetActive(false);
        //Debug.Log("End of conversation.");
    }
}

