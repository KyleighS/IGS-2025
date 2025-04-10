using UnityEngine;
using UnityEngine.Rendering;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public bool spawnEvidence;

    public void Start()
    {
        spawnEvidence = false;
    }

    public string GetDescription()
    {
        return "To start a conversation";
    }

    public void Interact()
    {
        Debug.Log("Trigger called");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //spawnEvidence = true;
    }
}
