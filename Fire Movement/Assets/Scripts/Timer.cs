using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 120;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeElapsedText;
    public GameObject endScreen;

    private float timeElapsed = 0;
    
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            timeElapsed = 120;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;

            // Pop up
            endScreen.SetActive(true);
            FormatAndDisplay(timeElapsed, timeElapsedText);
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        // Change colour
        if (timeToDisplay <= 15)
        {
            timeText.color = new Color32(210, 0, 0, 255);
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
