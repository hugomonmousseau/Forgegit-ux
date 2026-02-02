using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererAuto : MonoBehaviour
{
    LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.SetPosition(0, GetComponent<Squad>().squadApparence.transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    
}
