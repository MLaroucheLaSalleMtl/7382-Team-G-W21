using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class animalsattack : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform Player;
    [SerializeField] GameObject TriggerRange;
    [SerializeField] GameObject AttackRange;
    private float EnemyOffest;

    [SerializeField] private Image HPImage;
    [SerializeField] private Text HPText;

    [SerializeField] private float TimesAfterAttack;
    private bool FindPlayer;
    private bool InAttackRange;
    private bool CanAttack;

    private bool CanMove;

    [SerializeField] private float AttackRate;
    private float AttackTimer;

    [SerializeField] private float HP;
    private float Max_HP;

    private Animator Anim;

    private bool IfDead;

    public float HP1 { get => HP; set => HP = value; }
    public float Max_HP1 { get => Max_HP; set => Max_HP = value; }

    void Start()
    {
        Max_HP1 = HP1;
        agent = GetComponent<NavMeshAgent>();
        CanMove = true;
        Anim = GetComponent<Animator>();
        AttackTimer = AttackRate;

        CanAttack = true;
        IfDead = false;

    }
    private void SetHP()
    {
        HPImage.fillAmount = HP1 / Max_HP1;
        HPText.text = HP1 + "/" + Max_HP1;

    }
    private void Dead()
    {
        if (HP1 <= 0f && !IfDead)
        {
            CanMove = false;
            Anim.SetTrigger("IsDead");
            IfDead = true;
            Destroy(this.gameObject, 3f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
        SetHP();

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
