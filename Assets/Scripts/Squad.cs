using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    [Range(0, 1)]
    public float squadFactor;

    [Header("Apparence")]
    [SerializeField] GameObject squadApparence;
    [SerializeField] float maxSize;
    [SerializeField] float minSize;
    CameraZoomController zoomController;
    float lastRatio;
    void Start()
    {
        zoomController = Camera.main.GetComponentInParent<CameraZoomController>();
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = (zoomController.currentZoomZ - Mathf.Abs(zoomController.minZoomZ)) / zoomController.maxZoomZ;
        if (ratio == lastRatio) return;
        lastRatio = ratio;

        float scale = Mathf.Lerp(minSize, maxSize, ratio);
        squadApparence.transform.localScale = new Vector3(scale, scale, scale);
    }
}
