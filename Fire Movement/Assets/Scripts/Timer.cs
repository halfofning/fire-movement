using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //public float timeValue;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeElapsedText;
    public GameObject endScreen;

    private float timeElapsed = 0;
    //private float maxTime;
    //public static bool isTimeRanOut = false;

    //void Start()
    //{
    //    maxTime = timeValue;
    //}

    void Update()
    {
        if (TimerTrigger.triggered)
        {
            timeElapsed += Time.deltaTime;
        }

        DisplayTime(timeElapsed);
    }

    void DisplayTime(float timeToDisplay)
    {
        // Pop up when all rooms have been cleared
        if (AllyMovement._roomsLeft == 0)
        {
            endScreen.SetActive(true);
            FormatAndDisplay(timeElapsed, timeElapsedText);
        }

        FormatAndDisplay(timeToDisplay, timeText);
    }
    
    void FormatAndDisplay(float time, TextMeshProUGUI TMPtext)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        TMPtext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
