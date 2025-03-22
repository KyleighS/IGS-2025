using UnityEngine;

public class ForestSceneEvidence : MonoBehaviour
{
    public TestDialogueEvidence test;
    public GameObject testEvidence;

    public void Start()
    {
        if(test.evidenceActive)
        {
            Debug.Log("Evidence is active");
            test.evidenceActive = true;
        }
        else
        {
            Debug.Log("Evidence is active");
            test.evidenceActive = false;
        }
    }
}
