using NUnit.Framework;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> inventory;
    public List<Sprite> pictures; 
    public List<GameObject> picSlots;
    public List<GameObject> creatureHidePoints;

    public GameObject winOverlay;
    public GameObject camOverlay;
    public int allEvidence;
    public Slider evidenceSlider;
    public string nextScene;
    public TimeController timeController;


    private float lerpSpeed = 0.7f;
    private float lerp = 0;
    private float startLerp = 0;
    public float endLerp = 0;
    public int picutresTaken = 0;

    private void Start()
    {
        Time.timeScale = 1f;
        evidenceSlider.maxValue = allEvidence;
        evidenceSlider.value = 0;
        winOverlay.SetActive(false);
    }

    public void Update()
    {
        lerp += lerpSpeed * Time.deltaTime;
        //Debug.Log("Preclamp: " + lerp);
        lerp = Mathf.Clamp(lerp, 0, endLerp);
        evidenceSlider.value = lerp;
        //Debug.Log(lerp);

    }

    public void UpdateEvidence()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].gameObject.tag == "Evidence")
            {
                updateLerp();
            }

        }
    }

    public float updateLerp()
    {
        endLerp = inventory.Count + picutresTaken;
        
        if (endLerp == allEvidence)
        {
            winOverlay.SetActive(true);
            timeController.warnningTxt.SetActive(false);
            camOverlay.SetActive(false);
            if (SceneManager.GetActiveScene().name == "Night4")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                timeController.warnningTxt.SetActive(false);
                camOverlay.SetActive(false);

            }
        }
        return endLerp;
    }
}

