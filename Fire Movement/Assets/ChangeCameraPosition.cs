using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(0, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
