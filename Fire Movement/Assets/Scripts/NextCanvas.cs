using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class NextCanvas : MonoBehaviour
{
    public GameObject[] canvasList;
    public int currentCanvas = 0;
    public SteamVR_Action_Boolean menuControlLeft;
    public SteamVR_Action_Boolean menuControlRight;

    // todo:
    // code left and right canvas using the canvas list
    // through steam vr input

    private void Update()
    {
        if (menuControlRight.lastStateDown)
        {
            GetNext();
            Debug.Log("Right");
        }
        else if (menuControlLeft.lastStateDown) 
        {
            GetPrevious();
            Debug.Log("Left");
        }

        canvasList[currentCanvas].SetActive(true);
        Debug.Log(currentCanvas);
    }

    public void GetNext()
    {
        if (currentCanvas == 10)
            currentCanvas = 10;
        else 
        {
            canvasList[currentCanvas].SetActive(false);
            currentCanvas++;
        }
    }

    public void GetPrevious()
    {
        if (currentCanvas == 0)
            currentCanvas = 0;
        else
        {
            canvasList[currentCanvas].SetActive(false);
            currentCanvas--;
        }
    }
}
