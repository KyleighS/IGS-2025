using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections.Generic;

[System.Serializable]
public class CameraUI : MonoBehaviour
{
    public GameObject camOverlay;
    public CameraModes cameraModes;

    [Header("Record dot")]
    public Image dot;
    public Color newColor;
    public Color ogColor;
    public Color tempColor;
    public float fadeTime = 0.1f;
    public bool decrease = true;

    [Header("Timer")]
    public TextMeshProUGUI timerTxt;
    private float timeCount;

    [Header("Battery")]
    public int batteryCharge;
    public int fullBattery = 100;
    public int drainRate;
    public Slider batterySlider;
    private bool activated = true;
    public GameManager gameManager;
    public TextMeshProUGUI batCountTxt;

    void Start()
    {
        tempColor = dot.color;
        batteryCharge = fullBattery;
    }

    private void Update()
    {
        Timer();
        batCountTxt.text = GetBatteryCount().ToString();


        //checks if the dot color is not the new color and that the aplha is decreasing
        if (dot.color != newColor && tempColor.a > 0 && decrease)
        {
            //subtracts form the alpha in tempColor and sets it to the dot color
            tempColor.a = tempColor.a - (float)0.01;
            dot.color = tempColor;
        }
        else
        {
            decrease = false;
        }

        //checks if the dot color is not the original color and that the aplha is not decreasing
        if (dot.color != ogColor && tempColor.a < 1 && !decrease)
        {
            //adds to the alpha in tempColor and sets it to the dot color
            tempColor.a = tempColor.a + (float)0.01;
            dot.color = tempColor;
        }
        else
        {
            decrease = true;
        }
        //if (batteryCharge <= 0)
        //{
        //    camOverlay.SetActive(false);
        //    bool volumeGrain = cameraModes.volume.profile.TryGet(out FilmGrain grain);
        //    grain.active = !grain.active;
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ReloadBattery();
        //}

    }

    public void Timer()
    {

        timeCount += Time.deltaTime;
        int hours = Mathf.FloorToInt(timeCount / 3600) % 24;
        int mins = Mathf.FloorToInt(timeCount / 60) % 60;
        int sec = Mathf.FloorToInt(timeCount % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}:{2:00}", hours, mins, sec);


        if (sec % 5 == 0 && !activated)
        {
            batteryCharge -= drainRate;
            batterySlider.value = batteryCharge;
            //Debug.Log("Slider was updated");
            activated = true;
            //Debug.Log("Charge remaining: " + batteryCharge);
        }
        else if (sec % 5 == 1)
        {
            activated = false;
        }
    }
    public void ReloadBattery()
    {
        for (int i = 0; i < gameManager.inventory.Count; i++)
        {
            if (gameManager.inventory[i].gameObject.tag == "Battery")
            {
                batteryCharge = fullBattery;
                batterySlider.value = batteryCharge;
                gameManager.inventory.Remove(gameManager.inventory[i]);
                //Debug.Log("Player has battery");
            }
            else
            {
                //Debug.Log("Player dosent have a battery");
            }
        }


    }
    public int GetBatteryCount()
    {
        int batteryCount = 0;

        return batteryCount;
    }
}




