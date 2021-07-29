using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    private float deactivateTime = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ally"))
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                //StartCoroutine(TimeDeactivate(deactivateTime));
            }
            else if (closeTrigger)
            {
                myDoor.Play("DoorClose", 0, 0.0f);
            }
        }
    }

    //private IEnumerator TimeDeactivate(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    myDoor.Play("DoorClose", 0, 0.0f);
    //}
}
