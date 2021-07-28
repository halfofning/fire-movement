using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle1Trigger : MonoBehaviour
{
    public static bool triggered = false;
    public GameObject particle;
    public Material collidedMaterial;
    public AudioClip bellJingleSound;

    private bool played = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !played) 
        {
            Debug.Log("collided");
            triggered = true;
            particle.SetActive(false);

            // Change reticle colour
            GetComponent<MeshRenderer>().material = collidedMaterial;

            // Play some bell sound here
            if (!played)
            {
                AudioSource.PlayClipAtPoint(bellJingleSound, transform.position);
                played = true;
            }

            ShoulderTap.isTappable = true;
        }
    }
}