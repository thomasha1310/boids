using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 avoidanceMove = Vector2.zero;
        int numberOfAvoidedObjects = 0;
        foreach (Transform item in context)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                numberOfAvoidedObjects++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }
        if (numberOfAvoidedObjects > 0)
        {
            avoidanceMove /= numberOfAvoidedObjects;
        }

        return avoidanceMove;
    }
}
