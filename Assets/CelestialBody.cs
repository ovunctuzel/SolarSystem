using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass = 1;
    public Vector3 initialVelocity;
    
    [Range(0, 1)]
    public float rotationSpeed;

    public bool visualizeTrajectory;
    [Range(0, 5000)]
    public int visualizationHorizon = 20;
    [Range(1, 50)]
    public int visualizationPeriod = 1;
    public CelestialBody simulationOrigin;

    private Vector3 velocity;
    private Vector3 position;
    
    void Reset()
    {
        SimulationManager.Get().ResetBodyList();
    }

    void Start()
    {
        InitializeState();
        if(GetComponent<LineRenderer>())
        {
            GetComponent<LineRenderer>().enabled = false;
        }
    }

    void InitializeState()
    {
        position = transform.position;
        velocity = initialVelocity;
    }

    void Update()
    {
        SimulateStep(SimulationManager.Get().simulationTimeStep);
        transform.position = position;
        transform.Rotate(Vector3.up, rotationSpeed);
    }

    void SimulateStep(float dt)
    {
        // F = ma
        Vector3 acceleration = GetNetForce() / mass;
        velocity += acceleration * dt;
        position += velocity * dt;
    }

    Vector3 GetNetForce()
    {
        // F = GmM/d^2
        Vector3 netForce = Vector3.zero;
        foreach (CelestialBody body in SimulationManager.Get().celestialBodies)
        {
            if(body == this) continue;
            
            Vector3 relativePosition = body.position - position;
            float sqrDistance = relativePosition.sqrMagnitude;
            Vector3 direction = relativePosition.normalized;

            netForce += direction * (SimulationManager.Get().gravitationalConstant * body.mass * mass / sqrDistance);
        }

        return netForce;
    }

    void OnValidate()
    {
        if(Application.isPlaying) return;

        if(!visualizeTrajectory)
        {
            GetComponent<LineRenderer>().enabled = false;
            return;
        }
        else
        {
            GetComponent<LineRenderer>().enabled = true;
        }

        visualizationHorizon = Mathf.Clamp(visualizationHorizon, 0, 10000);
        GetComponent<LineRenderer>().positionCount = visualizationHorizon / visualizationPeriod;

        List<Vector3> positions = new List<Vector3>();
        SimulationManager.Get().celestialBodies.ForEach((x) => x.InitializeState());
        for (int i = 0; i < visualizationHorizon; i++)
        {
            foreach (CelestialBody body in SimulationManager.Get().celestialBodies)
            {
                body.SimulateStep(SimulationManager.Get().simulationTimeStep);
                Vector3 pointToDraw = body.position;

                if(i % visualizationPeriod == 0 && body == this)
                {
                    if(simulationOrigin)
                    {
                        pointToDraw -= simulationOrigin.position - simulationOrigin.transform.position;
                    }

                    positions.Add(pointToDraw);
                }
            }
        }
        GetComponent<LineRenderer>().SetPositions(positions.ToArray());
    }
}
