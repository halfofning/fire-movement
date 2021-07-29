using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grading : MonoBehaviour
{
    public TextMeshProUGUI successText;
    public static bool success = true;

    void Update()
    {
        DisplayText();
    }

    void DisplayText()
    {
        string word = "";
        if (success)
            word = "SUCCESS";
        else
            word = "FAILURE";
    
        successText.text = string.Format("MISSION {0}!", word);
    }
}
