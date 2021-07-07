using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0 || transform.position.y > 0.1)
        {
            transform.position = new Vector3(transform.position.x, 0.04f, transform.position.z);
        }
    }
}
