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

        Collider[] hitColliders = Physics.OverlapSphere(controller.transform.position, 1f);
        foreach (Collider col in hitColliders)
        {
            if (col != null && col.CompareTag("Player"))
            {
                controller._enemy.Attack(col.GetComponent<Character>());
            }
        }
    }
}
