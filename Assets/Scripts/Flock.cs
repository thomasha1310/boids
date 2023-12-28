using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maximumSpeed = 5f;
    [Range(1f, 10f)]
    public float detectionRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareDetectionRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareDetectionRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maximumSpeed * maximumSpeed;
        squareDetectionRadius = detectionRadius * detectionRadius;
        squareAvoidanceRadius = squareDetectionRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360)),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add( newAgent );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
