using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoomController : MonoBehaviour
{
    [Header("Camera & Pivot")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform pivotTransform;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float zoomSmoothness = 5f;

    [SerializeField] private float minZoomZ = -5f;
    [SerializeField] private float maxZoomZ = -30f;

    [Header("Pivot Rotation Settings")]
    [SerializeField] private float minPivotX = 30f;
    [SerializeField] private float maxPivotX = 75f;

    private float targetZoomZ;
    private float currentZoomZ;

    void Start()
    {
        currentZoomZ = cameraTransform.localPosition.z;
        targetZoomZ = currentZoomZ;
    }

    void Update()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) > 0.01) Debug.Log(scroll + " " + scroll * zoomSpeed * Time.deltaTime);

        if (Mathf.Abs(scroll) > 0.01f)
        {
            targetZoomZ += scroll * zoomSpeed * Time.deltaTime;
            targetZoomZ = Mathf.Clamp(targetZoomZ, maxZoomZ, minZoomZ);
        }

        // Lissage du zoom
        currentZoomZ = Mathf.Lerp(currentZoomZ, targetZoomZ, Time.deltaTime * zoomSmoothness);
        Vector3 localPos = cameraTransform.localPosition;
        localPos.z = currentZoomZ;
        cameraTransform.localPosition = localPos;

        // Rotation X du pivot en fonction du zoom
        float t = Mathf.InverseLerp(minZoomZ, maxZoomZ, currentZoomZ);
        float targetAngleX = Mathf.Lerp(minPivotX, maxPivotX, t);
        Vector3 pivotEuler = pivotTransform.localEulerAngles;
        pivotEuler.x = targetAngleX;
        pivotTransform.localEulerAngles = pivotEuler;
    }
}