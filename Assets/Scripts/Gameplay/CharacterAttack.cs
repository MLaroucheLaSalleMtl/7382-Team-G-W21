using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : CharacterAction
{
    public float attackCD;
    private bool _isAttacking;
    [SerializeField] private ActionTrigger _actionTrigger;

    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _actionTrigger.actionFeedback.AddListener(_character.DoDamage);
    }

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
        _actionTrigger.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _actionTrigger.gameObject.SetActive(false);
        yield return new WaitForSeconds(attackCD - 0.1f);
        _isAttacking = false;
    }
}
