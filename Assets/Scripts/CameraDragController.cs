using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDragController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float dragSpeed = 1.0f;

    private Vector3? dragStartWorldPos = null;
    private Plane dragPlane;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        // Plan horizontal à Y = 0
        dragPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            dragStartWorldPos = GetMouseWorldPosition();
        }

        if (Mouse.current.leftButton.isPressed && dragStartWorldPos.HasValue)
        {
            Vector3 currentWorldPos = GetMouseWorldPosition();
            Vector3 delta = dragStartWorldPos.Value - currentWorldPos;

            // Déplacement de la caméra sur XZ
            Vector3 move = new Vector3(delta.x, 0, delta.z);
            transform.position += move * dragSpeed;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            dragStartWorldPos = null;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (dragPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }

        return Vector3.zero;
    }
}