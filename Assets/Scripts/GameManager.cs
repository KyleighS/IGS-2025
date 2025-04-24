using NUnit.Framework;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> inventory;
    public List<Sprite> pictures; 
    public List<GameObject> picSlots;
    public List<GameObject> creatureHidePoints;

    public GameObject winOverlay;
    public int allEvidence;
    public Slider evidenceSlider;
    public string nextScene;

    private void Start()
    {
        evidenceSlider.maxValue = allEvidence;
        evidenceSlider.value = 0;
        winOverlay.SetActive(false);
    }

    public void Update()
    {
          
    }

    public void UpdateEvidence()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].gameObject.tag == "Evidence")
            {
                if (evidenceSlider.value == allEvidence)
                {
                    winOverlay.SetActive(true);
                }

                evidenceSlider.value++;
            }

        }
    }

}

