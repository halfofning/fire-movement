using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerTrigger : MonoBehaviour
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

            // Play some bell sound here to signal start of timer
            if (!played)
            {
                AudioSource.PlayClipAtPoint(bellJingleSound, transform.position);
                played = true;
            }
        }
    }
}
