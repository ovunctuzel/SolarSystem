using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager instance;

    public float gravitationalConstant = 6.67408e-11f;
    public float simulationTimeStep = 0.01f;
    public List<CelestialBody> celestialBodies;

    public static SimulationManager Get()
    {
        if(Application.isPlaying && instance)
        {
            return instance;
        }
        else
        {
            return FindObjectOfType<SimulationManager>();
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple singletons detected!");
        }

        ResetBodyList();
    }

    void Reset()
    {
        ResetBodyList();
    }

    public void ResetBodyList()
    {
        celestialBodies.Clear();
        foreach (var body in FindObjectsOfType<CelestialBody>())
        {
            celestialBodies.Add(body);
        }
    }
}
