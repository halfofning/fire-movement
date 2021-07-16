using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle3Trigger : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            Debug.Log("collided");
            triggered = true;
            particle.SetActive(false);

            // Change reticle colour
            GetComponent<MeshRenderer>().material = collidedMaterial;

            // Set shoulder to be tappable when the next reticle is active: see ShoulderTap.cs
            Debug.Log("Setting shoulder tap to be active");
            ShoulderTap.isTappable = true;

            // TODO: Play some bell sound here to signal start of timer
        }
    }
}
