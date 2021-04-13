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
    [SerializeField] Image HPimage;
    [SerializeField] Text hptext;
    [SerializeField] private float hp;
    [SerializeField] private float TimesAfterAttack;
    private bool FindPlayer;
    private bool InAttackRange;
    private bool CanAttack;

    private bool CanMove;
    private float max_hp;

    [SerializeField] private float AttackRate;
    private float AttackTimer;

    private Animator Anim;

    public float Hp { get => hp; set => hp = value; }
    public float Max_hp { get => max_hp; set => max_hp = value; }

    void Start()
    {
        max_hp = Hp;
        agent = GetComponent<NavMeshAgent>();
        CanMove = true;
        Anim = GetComponent<Animator>();
        AttackTimer = AttackRate;

        CanAttack = true;

    }
    private void setHP()
    {
        HPimage.fillAmount = Hp / Max_hp;
        hptext.text = Hp + "/" + Max_hp;
    }
    // Update is called once per frame
    void Update()
    {
        setHP();
        //Debug.Log(CanMove);

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
