using UnityEngine;
using UnityEngine.Rendering;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public Collider boxCollider;

    public string GetDescription()
    {
        return "To start a conversation";
    }

    public void Interact()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        boxCollider.enabled = false;
    }
}
