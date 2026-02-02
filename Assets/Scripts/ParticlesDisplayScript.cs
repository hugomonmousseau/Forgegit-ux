using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDisplayScript : MonoBehaviour
{
    public List<Material> materials;
    [SerializeField] Gradient gradiant;
    [SerializeField] CameraZoomController zoomController;
    float lastRatio;

    private void Update()
    {
        float ratio = (zoomController.currentZoomZ - Mathf.Abs(zoomController.minZoomZ)) / zoomController.maxZoomZ;
        if (ratio == lastRatio) return;
        lastRatio = ratio;

        foreach(Material mat in materials)
        {
            mat.SetColor("_Color",new Color(mat.color.r, mat.color.g, mat.color.b, gradiant.Evaluate(ratio).a));
        }
    }



}
