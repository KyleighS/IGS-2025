using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ColorFlash : MonoBehaviour
{
    public GameObject camOverlay;
    [Header("Record dot")]
    public Image dot;
    public Color newColor;
    public Color ogColor;
    public float fadeTime = 0.1f;
    public bool decrease = true;

    [Header("Timer")]
    public TextMeshProUGUI timerTxt;
    private float timeCount;

    [Header("Battery")]
    public float batteryCharge;
    public float drainRate = 10.0f;
    public Slider batterySlider;

    void Start()
    {
        batteryCharge = 100.0f;    // full charge
    }

    private void Update()
    {
        Timer();
        BatteryDrain();


        Color tempColor = dot.color;
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

    }

    public void Timer()
    {
        timeCount += Time.deltaTime;
        int hours = Mathf.FloorToInt(timeCount / 3600) % 24;
        int mins = Mathf.FloorToInt(timeCount / 60) % 60;
        int sec = Mathf.FloorToInt(timeCount % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}:{2:00}", hours, mins, sec);
    }

    public void BatteryDrain()
    {
        float usageThisFrame = drainRate * Time.deltaTime;

        batteryCharge -= usageThisFrame;

        Debug.Log("Charge remaining: " + batteryCharge);

        if (batteryCharge <= 0)
        {
            camOverlay.SetActive(false);
        }
    }
}


