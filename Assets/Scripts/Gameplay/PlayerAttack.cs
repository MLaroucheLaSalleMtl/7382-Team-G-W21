using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float attackCD;
    private bool _isAttacking;
    [SerializeField] private Feedback _feedback;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }

    }

    IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _feedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _feedback.gameObject.SetActive(false);
        yield return new WaitForSeconds(attackCD - 0.1f);
        _isAttacking = false;
    }
}
