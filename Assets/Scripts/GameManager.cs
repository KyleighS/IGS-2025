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

    public GameObject winOverlay;
    public int allEvidence = 5;
    public Slider evidenceSlider;

    private void Start()
    {
        evidenceSlider.maxValue = allEvidence;
        evidenceSlider.value = 0;
    }

    public void Update()
    {
        //foreach (KeyValuePair<string, bool> item in inventory)
        //{
        //    if (item.Key == "Evidence" && item.Value)
        //    {
        //        Debug.Log("Key " + item.Key + " Value " + item.Value);
        //        evidenceSlider.value++;
        //        inventory[item.Key] = false;
        //        Debug.Log("Key " + item.Key + " Value " + item.Value);

        //    }
        ////}
        //for (int i = 0; i < inventory.Count; i++)
        //{
        //    if (inventory[i].gameObject.tag == "Evidence")
        //    {
        //        if (evidenceSlider.value >= allEvidence)
        //        {
        //            winOverlay.SetActive(true);
        //        }

        //        evidenceSlider.value++;
        //    }

        //}
    }

    public void UpdateEvidence()
    {
        //foreach (KeyValuePair<string, bool> item in inventory)
        //{
        //    if (item.Key == "Evidence" && item.Value)
        //    {
        //        Debug.Log("Key " + item.Key + " Value " + item.Value);
        //        evidenceSlider.value++;
        //        inventory[item.Key] = false;
        //        Debug.Log("Key " + item.Key + " Value " + item.Value);

        //    }
        //}

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].gameObject.tag == "Evidence")
            {
                if (evidenceSlider.value >= allEvidence)
                {
                    winOverlay.SetActive(true);
                }

                evidenceSlider.value++;
            }

        }
    }

}

