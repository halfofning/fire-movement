using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // Use default or distance
        float targetLength = m_DefaultLength;

        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default
        Vector2 endPosition = transform.position + (transform.forward * targetLength);

        // Or based on hit collider
        if (hit.collider != null)
            endPosition = hit.point;

        // Set position of the dot
        m_Dot.transform.position = endPosition;

        // Set line renderer
        Debug.Log("initial Position: " + transform.position.x + transform.position.y + transform.position.z);
        Debug.Log("end Position: " + endPosition);
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        Debug.Log(hit);
        return hit;
    }
}
