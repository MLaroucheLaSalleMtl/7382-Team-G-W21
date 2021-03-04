using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * controller.lookRange, Color.green);

        if (Physics.SphereCast(controller.transform.position, controller.lookSphereCastRadius, controller.transform.forward, out RaycastHit hit, controller.lookRange) && hit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = hit.transform;
            return true;
        }

        return false;
    }
}
