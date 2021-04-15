using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Character _character;

    public StatsScriptable statsScriptable;

    public bool activeAI;
    public List<Transform> wayPoints;
    private StateController _stateController;

    public Animator anim;

    public float attackDelay;
    private bool _isAttacking;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseStamina, statsScriptable.BaseDefense);

        _stateController = GetComponent<StateController>();
    }

    private void Start()
    {
        if (_stateController != null)
            _stateController.SetupAI(activeAI, wayPoints, this);
    }

    public void Attack(Character entity)
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _character.DoDamage(entity);
            anim.SetTrigger("Attack1");
            if (entity.IsDead)
                _stateController.chaseTarget = null;

            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackDelay);
        _isAttacking = false;
    }

    void Finish_Attack()
    {
        //Debug.Log("Finish Attack");
        //{
        //    attack_count++;
        //    if (attack_count == max_attack_count)
        //    {
        //        CanAttack2 = true;
        //        Reset_Attack();
        //    }
        //}
    }
}
