using System;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private float multiplier;
    [SerializeField]
    private float startHour;
    [SerializeField] 
    private float sunriseHour;
    [SerializeField]
    private float sunsetHour;
    [SerializeField]
    private TextMeshProUGUI timeTxt;

    bool pauseTimer;

    public DateTime curTime;
    public GameObject warnningTxt;
    public GameObject endScreen;
    public PauseMenu pauseScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        warnningTxt.SetActive(false);
        endScreen.SetActive(false);
        //sunriseTime = TimeSpan.FromHours(sunriseHour);
        //sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if(!pauseTimer)
        {
            curTime = curTime.AddSeconds(Time.deltaTime + multiplier);

            if(timeTxt != null )
            {
                timeTxt.text = curTime.ToString("HH:mm");
            }
        }

        if (curTime.Hour == sunriseHour - 2)
        {
            warnningTxt.SetActive(true);
        }

        if(curTime.Hour == sunriseHour)
        {
            warnningTxt.SetActive(false);
            endScreen.SetActive(true);
            pauseTimer = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }

    public void PauseTimer() => pauseTimer = true;
    public void UnpauseTimer() => pauseTimer = false;

}
