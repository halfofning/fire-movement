using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderTap : MonoBehaviour
{
    public float speed;
    public List<GameObject> targetDoors;
    public GameObject thirdAlly;
    public bool tapped;
    public static bool isTappable = true;

    private int currTargetDoor = 0;
    private int totalDoors;

    private void Start()
    {
        totalDoors = targetDoors.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered but " + isTappable);
        if (other.CompareTag("Player") && isTappable)
        {
            Debug.Log("shoulder tap");
            tapped = true;
        }
    }

    private void Update()
    {
        if (tapped && currTargetDoor < totalDoors && isTappable)
        {
            ThirdMoveTowardsDoor();
        }
    }

    // Third man moves towards the door when tapped by hand.
    private void ThirdMoveTowardsDoor()
    {
        GameObject targetDoor = targetDoors[currTargetDoor];

        thirdAlly.GetComponent<Animator>().Play("Rifle Walk");
        thirdAlly.transform.position = Vector3.MoveTowards(thirdAlly.transform.position, targetDoor.transform.position, speed * Time.deltaTime);

        //Debug.Log(Vector3.Distance(thirdAlly.transform.position, targetDoor.transform.position));
        Debug.Log(Vector3.kEpsilon);

        if (Vector3.Distance(thirdAlly.transform.position, targetDoor.transform.position) < Vector3.kEpsilon)
        {
            thirdAlly.GetComponent<Animator>().Play("Idle");
            
            Debug.Log("move on to next door");
            tapped = false;

            // Set the shoulder tap as inactive, and set this active again when the next reticle is activated
            isTappable = false;

            // If already opened the door, change the next targetDoor to open
            currTargetDoor++;
        }
    }
}
