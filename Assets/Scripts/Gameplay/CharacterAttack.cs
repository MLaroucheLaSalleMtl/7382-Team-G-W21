using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : CharacterAction
{
    public float attackCD;
    private bool _isAttacking;
    [SerializeField] private ActionTrigger _actionTrigger;
     
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _actionTrigger.actionFeedback.AddListener(_characterController.Character.DoDamage);
    }

    /// <summary>
    /// Input OnAttack event
    /// </summary>
    /// <param name="context"></param>
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_isAttacking)
            {
                _characterController.SpendEnergy(5, () => StartCoroutine(AttackRoutine()));
            }
        }
    }

    /// <summary>
    /// Attack delay
    /// </summary>
    /// <returns></returns>
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
