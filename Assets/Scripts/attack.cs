using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class attack : Enemy
{
    NavMeshAgent agent;
    public Transform Target;
    [SerializeField] private float AttackDistance;
    [SerializeField] private float Attacktimes;
    private float CoolDown;
    private Animator anim;
    private bool canattack;
    private float enemyoffset;
    float timer;
    bool space;
    bool IsAttack;
    int attack_count;
    int max_attack_count;
    private bool IsDead;
    private bool CanMove;

    private bool CanAttack2;


    void Start()
    {
        CanAttack2 = false;
        agent = GetComponent<NavMeshAgent>();
        CoolDown = Attacktimes;
        timer = Attacktimes;
        anim = GetComponent<Animator>();
        canattack = true;
        set_property();
        Set_Enemy();
        space = false;
        attack_count = 1;
        max_attack_count = 4;
        IsDead = false;
        CanMove = true;
    }
    void Finish_Attack()
    {
        Debug.Log("Finish Attack");
        {

            attack_count++;
            if (attack_count == max_attack_count)
            {
                CanAttack2 = true;
                Reset_Attack();
            }
        }
    }
    void die()
    {

        anim.SetTrigger("IsDead");

        Destroyself();
    }
    void Destroyself()
    {
        Destroy(transform.gameObject, 2.0f);
    }
    void Reset_Attack()
    {
        attack_count = 0;
    }
    void Update()
    {
        if (!CanMove)
        {
            agent.ResetPath();
        }

        Debug.Log("Attack Count" + attack_count);
        enemyoffset = Vector3.Distance(this.transform.position, Target.transform.position);
        if (!canattack)
        {
            resetattack();
        }
        animMove();
        if (space)
        {
            if (IsAttack)
            {
                Receivedam();
            }
        }
        if (!space)
        {
            IsAttack = true;
        }
        if (Hp < 0)
        {
            IsDead = true;

        }

        if (IsDead)
        {
            IsDead = false;
            Hp = 0f;
            CanMove = false;
            die();
        }
    }
    public void OnSpace(InputAction.CallbackContext context)
    {
        space = context.performed;
    }
    private void Receivedam()
    {
        float damage = 4.0f;
        if (Hp > 0)
        {

            Hp -= damage;


        }

        IsAttack = false;
    }
    private void set_property()
    {
        this.Hp = 10;
        this.Damage = 1;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.LookAt(Target.position);
            if (enemyoffset <= AttackDistance)
            {
                canattack = true;
                agent.ResetPath();
                if (canattack)
                {
                    if (!CanAttack2)
                    {
                        anim.SetTrigger("Attack1");
                    }
                    if (CanAttack2)
                    {
                        anim.SetTrigger("Attack2");
                        CanAttack2 = false;
                    }
                    canattack = false;
                    timer = Attacktimes;
                }
            }
            if (enemyoffset > AttackDistance)
            {
                //anim.SetBool("Move", true);
                agent.SetDestination(Target.position);
            }
        }
    }
    private void animMove()
    {
        float maginitude;
        maginitude = agent.velocity.magnitude;
        anim.SetFloat("Magnitude", maginitude);
    }
    private void resetattack()
    {
        timer -= 1.0f * Time.deltaTime;
        if (timer <= 0.0f)
        {
            canattack = true;
            CoolDown = Attacktimes;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            agent.ResetPath();
        }


    }
}