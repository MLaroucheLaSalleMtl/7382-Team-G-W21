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
    private bool FindEnemy;

    public bool FindEnemy1 { get => FindEnemy; set => FindEnemy = value; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CoolDown = Attacktimes;
        timer = Attacktimes;
        anim = GetComponent<Animator>();
        canattack = true;
        set_property();
        Set_Enemy();
        space = false;
        FindEnemy = false;

        attack_count = 0;
        max_attack_count = 3;
        IsDead = false;
    }
    void Finish_Attack()
    {
        Debug.Log("Finish Attack");
        {
            attack_count++;
            if (attack_count > max_attack_count)
            {
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
        Destroy(transform.gameObject, 3.0f);
    }
    void Reset_Attack()
    {
        attack_count = 0;
    }
    // Update is called once per frame
    void Update()
    {
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
        if (Hp <= 0)
        {
            IsDead = true;
        }
        //Debug.Log(IsDead);
        if (IsDead)
        {
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
        if (Hp <= 0)
        {
            Hp = 0;
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
                    if (attack_count != 2)
                    {
                        anim.SetTrigger("Attact");
                    }
                    if (attack_count == 2)
                    {
                        anim.SetTrigger("Attack2");
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
        anim.SetBool("FindEnemy", FindEnemy);
    }
    private void resetattack()
    {
        timer -= 1.0f * Time.deltaTime;
        //Debug.Log(timer);
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