using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/DistanceDecision")]
public class DistanceDecision : Decision
{
    public float farDistance;

    public override bool Decide(StateController controller)
    {
        float distance = Vector3.Distance(controller.transform.position, controller.chaseTarget.position);
        if (distance <= farDistance)
        {
            return true;
        }
        return false;
    }
}
