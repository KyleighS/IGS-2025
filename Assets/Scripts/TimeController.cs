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
    private Light sunLight;
    [SerializeField] 
    private float sunriseHour;
    [SerializeField]
    private float sunsetHour;
    [SerializeField]
    private TextMeshProUGUI timeTxt;
    [SerializeField]
    private Color dayAmbientLight;
    [SerializeField]
    private Color nightAmbientLight;
    [SerializeField]
    private AnimationCurve lightChangeCurve;
    [SerializeField]
    private float maxSunlightIntensity;
    [SerializeField]
    private Light moonLight;
    [SerializeField]
    private float maxMoonlightIntensity;

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;
    bool pauseTimer;

    public DateTime curTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        RotateSun();
        UpdateLighting();
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
    }

    private TimeSpan CalculateTimeDiff(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan diff = fromTime - toTime;

        if(diff.TotalSeconds > 0 && !pauseTimer)
        {
            diff += TimeSpan.FromHours(24);

        }
        return diff;
    }

    public void PauseTimer() => pauseTimer = true;
    public void UnpauseTimer() => pauseTimer = false;

    private void RotateSun()
    {
        float sunLightRotation;

        if(curTime.TimeOfDay > sunriseTime && curTime.TimeOfDay < sunsetTime) 
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDiff(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDiff(sunriseTime, curTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);

        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDiff(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDiff(sunsetTime, curTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLighting()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunlightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(0, maxMoonlightIntensity, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }
}
