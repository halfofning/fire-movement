using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenForAudioCommand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(MicInput.MicLoudness));
        Debug.Log("Volume is " + MicInput.MicLoudness.ToString("##.#####") + ", decibels is: " + db.ToString());
    }
}
