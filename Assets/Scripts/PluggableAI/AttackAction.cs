using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * controller.attackRange, Color.red);

        if (Physics.SphereCast(controller.transform.position, controller.lookSphereCastRadius, controller.transform.forward, out RaycastHit hit, controller.attackRange) && hit.collider.CompareTag("Player"))
        {
            if (controller.CheckIfCountDownElapsed(controller.attackRate))
            {
                // TODO::ATTACK -- Display attack animation
            }
        }
    }
}
