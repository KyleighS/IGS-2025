using UnityEngine;

public class ForestSceneEvidence : MonoBehaviour
{
    public TestDialogueEvidence test;
    public GameObject testEvidence;

    public void Start()
    {
        if(test.evidenceActive == true)
        {
            Debug.Log("Evidence is active");
            testEvidence.SetActive(true);
        }
        else
        {
            Debug.Log("Evidence is deactive");
            testEvidence.SetActive(false);
        }
    }
}
