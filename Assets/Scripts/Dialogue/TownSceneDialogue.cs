using UnityEngine;

public class TownSceneDialogue : MonoBehaviour
{
    public TestDialogueEvidence test;
    public DialogueTrigger dialogueTrigger;

    public void Update()
    {
        if (dialogueTrigger.spawnEvidence)
        {
            test.evidenceActive = true;
            Debug.Log("Evidence bool has been set to true");
        }
    }
    public void Start()
    {
        test.evidenceActive = false;
    }
}
