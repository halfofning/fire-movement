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

            //if (timeValue > 0)
            //{
            //timeValue -= Time.deltaTime;
            //}
            //else
            //{
            //    timeValue = 0;
            //    timeElapsed = maxTime;
            //    isTimeRanOut = true;
            //}
        }

        DisplayTime(timeElapsed);
    }

    void DisplayTime(float timeToDisplay)
    {
        // Pop up when all rooms have been cleared
        if (AllyMovement.roomsCleared == 5)
        {
            endScreen.SetActive(true);
            FormatAndDisplay(timeElapsed, timeElapsedText);
        }

        //if (timeToDisplay < 0)
        //{
        //timeToDisplay = 0;
        //}
        //else if (timeToDisplay > 0 && timeToDisplay != maxTime)
        //{
        //    timeToDisplay += 1;
        //}

        // Change colour
        //if (timeToDisplay < 16)
        //{
        //    timeText.color = new Color32(210, 0, 0, 255);
        //}

        FormatAndDisplay(timeToDisplay, timeText);
    }
    
    void FormatAndDisplay(float time, TextMeshProUGUI TMPtext)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        TMPtext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
