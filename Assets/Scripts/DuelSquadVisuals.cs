using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelSquadVisuals : MonoBehaviour
{
    [SerializeReference] List<GameObject> squads;
    [SerializeField] CameraZoomController zoomController;
    [SerializeField] List<GameObject> particles;
    private List<GameObject> currentparticles = new List<GameObject>();
    bool isDueling;

    [Range(0f, 1f)]
    [SerializeField] float squadFactor;

    float lastRatio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = (zoomController.currentZoomZ - Mathf.Abs(zoomController.minZoomZ)) / zoomController.maxZoomZ;
        if (ratio == lastRatio) return;
        lastRatio = ratio;

        if ((ratio > squadFactor && !isDueling) || (ratio < squadFactor && isDueling)) return;
        isDueling = !isDueling;

        if (isDueling) InstanciateParticleSystem();
        else DestroyParticleSystem();
    }

    void InstanciateParticleSystem()
    {
        Vector3 mid = (squads[0].GetComponent<Squad>().squadApparence.transform.position + squads[1].GetComponent<Squad>().squadApparence.transform.position ) / 2;
        for(int i = 0; i < 2; i++)
        {
            GameObject particle = Instantiate(particles[i], squads[i].transform.position, Quaternion.identity);
            particle.transform.LookAt(mid);
            currentparticles.Add(particle);
        }


    }

    void DestroyParticleSystem()
    {
        foreach(GameObject gameObject in currentparticles)
        {
            gameObject.Destroy();
        }
        currentparticles.Clear();
    }
}
