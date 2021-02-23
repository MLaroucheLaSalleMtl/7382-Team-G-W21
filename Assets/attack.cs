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
    public Transform[] Patrol_Positions;
    public bool IsInPosition;
    public bool IsPatrol;
    private float patrolPos_Offset;
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
        //IsInPosition = false;
        //int index = Random.Range(0, 2);
        //agent.destination = Patrol_Positions[index].position;
        //IsPatrol = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(canattack);
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
        //Patrol();
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
                agent.ResetPath();
                if (canattack)
                {
                    anim.SetTrigger("Attact");
                    canattack = false;
                    timer = Attacktimes;
                }
            }
            if (enemyoffset > AttackDistance)
            {
                anim.SetBool("Move", true);
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

    //private void Patrol()
    //{


    //    int index = Random_PatrolPos_Index();


    //    if (IsInPosition && !IsPatrol)
    //    {
    //        agent.destination = Patrol_Positions[index].position;
    //        patrolPos_Offset = Vector3.Distance(transform.position, agent.destination);
    //        if (patrolPos_Offset <= 1f)
    //        {
    //            IsPatrol = false;
    //        }
    //        if (patrolPos_Offset >= 1f)
    //        {
    //            IsPatrol = true;
    //        }


    //    }



    //}
}
