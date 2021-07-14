using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle1Trigger : MonoBehaviour
{
    public static bool triggered = false;
    public GameObject particle;
    public Material collidedMaterial;

    private void Update()
    {
        particle.SetActive(gameObject.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        triggered = true;
        particle.SetActive(false);

        // Change reticle colour
        GetComponent<MeshRenderer>().material = collidedMaterial;

        // TODO: Play some bell sound here to signal start of timer
    }
}
