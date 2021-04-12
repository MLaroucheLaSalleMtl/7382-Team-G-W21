using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animalsattack : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform Player;
    [SerializeField] GameObject TriggerRange;
    [SerializeField] GameObject AttackRange;
    private float EnemyOffest;

    [SerializeField] private float TimesAfterAttack;
    private bool FindPlayer;
    private bool InAttackRange;
    private bool CanAttack;

    private bool CanMove;

    [SerializeField] private float AttackRate;
    private float AttackTimer;

    private Animator Anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CanMove = true;
        Anim = GetComponent<Animator>();
        AttackTimer = AttackRate;

        CanAttack = true;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CanMove);

        FindPlayer = TriggerRange.GetComponent<AnimalTriggerRange>().FindPlayer1;
        InAttackRange = AttackRange.GetComponent<AnimalAttackRange>().InAttackRange1;

        SetNavMesh();
        ResetAttack();
        EnemyOffest = Vector3.Distance(transform.position, Player.position);
        if (FindPlayer)
        {
            if (CanMove)
            {
                agent.destination = Player.transform.position;
            }
            if (!CanMove)
            {
                agent.ResetPath();
            }
            if (InAttackRange)
            {

                if (CanAttack)
                {
                    CanMove = false;
                    CanAttack = false;
                    Attack();

                }

            }
        }
    }
    private void Attack()
    {

        Anim.SetTrigger("Attack");
        Invoke("ResetCanMove", TimesAfterAttack);

    }
    private void ResetCanMove()
    {
        CanMove = true;
    }
    private void ResetAttack()
    {
        if (!CanAttack)
        {
            AttackTimer -= Time.deltaTime;
            if (AttackTimer <= 0f)
            {
                CanAttack = true;
                AttackTimer = AttackRate;
            }
        }

    }
    private void SetNavMesh()
    {
        float Magnitude = agent.velocity.magnitude;
        Anim.SetFloat("Magnitude", Magnitude);
    }
}
