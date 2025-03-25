using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerTxt;
    [SerializeField]public float timeRemaining;

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
