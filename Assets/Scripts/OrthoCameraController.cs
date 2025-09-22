using UnityEngine;
using UnityEngine.InputSystem;

public class OrthoCameraDrag : MonoBehaviour
{
    [Header("Réglages")]
    public Camera cam;                // caméra orthographique
    public float inertiaDamping = 5f; // plus haut = la caméra s'arrête plus vite

    // Input system
    private PlayerControls controls;

    // Drag
    private bool isDragging = false;
    private Vector3 dragStartWorld;   // point sur le plan quand clic
    private Vector3 cameraStartPos;   // position initiale caméra
    private Vector3 dragVelocity;     // vitesse du drag (pour inertie)

    void Awake()
    {
        controls = new PlayerControls();

        // Clic gauche pressé
        controls.Camera.Click.started += ctx => StartDrag();
        // Clic gauche relâché
        controls.Camera.Click.canceled += ctx => EndDrag();

        // Mouse delta (pour calcul vitesse)
        controls.Camera.Drag.performed += ctx =>
        {
            if (isDragging)
                dragVelocity = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
        };
    }

    void OnEnable() => controls.Camera.Enable();
    void OnDisable() => controls.Camera.Disable();

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (!cam.orthographic)
            Debug.LogWarning("⚠️ La caméra n’est pas orthographique !");
    }

    void Update()
    {
        if (isDragging)
        {
            DragUpdate();
        }
        else
        {
            // inertie
            if (dragVelocity.sqrMagnitude > 0.01f)
            {
                transform.position += dragVelocity * Time.deltaTime;
                dragVelocity = Vector3.Lerp(dragVelocity, Vector3.zero, inertiaDamping * Time.deltaTime);
            }
        }
    }

    void StartDrag()
    {
        isDragging = true;
        dragVelocity = Vector3.zero;

        Vector3? world = GetMouseWorldPosition();
        if (world.HasValue)
        {
            dragStartWorld = world.Value;
            cameraStartPos = transform.position;
        }
    }

    void DragUpdate()
    {
        Vector3? world = GetMouseWorldPosition();
        if (world.HasValue)
        {
            Vector3 offset = dragStartWorld - world.Value;
            transform.position = cameraStartPos + offset;
        }
    }

    void EndDrag()
    {
        isDragging = false;
        // dragVelocity est déjà mise à jour par Mouse Delta
        // On la garde pour l’inertie
    }

    /// <summary>
    /// Projette la souris sur un plan Y=0 et retourne le point monde
    /// </summary>
    Vector3? GetMouseWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.up, Vector3.zero); // plan Y=0
        if (plane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return null;
    }
}
