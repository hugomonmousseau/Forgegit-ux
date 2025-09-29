using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLookController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private LayerMask raycastLayer = ~0; // Par défaut, tout


    private Vector3? dragStartWorldPos = null;
    private Plane dragPlane;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        dragPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {

            dragStartWorldPos = GetMouseWorldPosition();
        }
        if (Mouse.current.rightButton.isPressed && dragStartWorldPos.HasValue)
        {
            //ROTATIONNNNN

            //calculate distance

            //instantiate transform pivot

            //rotate pivot depending on drag distance

            //destroy transform pivot



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