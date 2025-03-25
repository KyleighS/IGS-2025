using UnityEngine;
using System.Collections.Generic;

public class ForestSceneEvidence : MonoBehaviour
{
    public TestDialogueEvidence test;
    public List<GameObject> testEvidence;

    public void Start()
    {
        Debug.Log("Evidence State: "+test.evidenceActive);
        if (test.evidenceActive == true)
        {
            for(int i = 0; i < testEvidence.Count; i++)
            {
                testEvidence[i].SetActive(true);
            }
            Debug.Log("Evidence is active");
        }
        else
        {
            Debug.Log("Evidence is deactive");
            for(int i = 0;i < testEvidence.Count; i++)
            {
                testEvidence[i].SetActive(false);
            }
        }
    }
}
