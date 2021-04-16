using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [SerializeField] private Image HPimage;
    [SerializeField] private Text HPText;
    private AudioSource[] Monsterssound;

    private bool CanAttack2;

    // Variables to handle the character stats
    public StatsScriptable statsScriptable;
    private Character _character;

    // Variable feedback for action trigger
    [SerializeField] private ActionTrigger meleeTrigger;
    [SerializeField] private ActionTrigger spellTrigger1;
    [SerializeField] private ActionTrigger spellTrigger2;

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
        Monsterssound = GetComponents<AudioSource>();

        // Initialize the character
        _character = GetComponent<Character>();
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseStamina, statsScriptable.BaseDefense);
        meleeTrigger.actionFeedback.AddListener(_character.DoDamage);
        spellTrigger1.actionFeedback.AddListener(_character.DoDamage);
        spellTrigger2.actionFeedback.AddListener(_character.DoDamage);
    }
    void Finish_Attack()
    {
        {
            // Deactivate the action trigger
            meleeTrigger.gameObject.SetActive(false);

            attack_count++;
            if (attack_count == max_attack_count)
            {
                CanAttack2 = true;
                Reset_Attack();
            }
        }
    }
    private void SetHp()
    {
        HPimage.fillAmount = Hp / Max_hp;
        HPText.text = Hp + "/" + Max_hp;
    }
    public void die()
    {

        anim.SetTrigger("IsDead");

        Destroyself();
    }
    public void Attackslime()
    {
        Monsterssound[0].Stop();
        Monsterssound[1].Play();
    }
    public void Turtleattack()
    {
        Monsterssound[0].Stop();
        Monsterssound[1].Play();
    }
    public void Turtleattack2()
    {
        Monsterssound[0].Stop();
        Monsterssound[1].Play();
    }
    void Destroyself()
    {
        Destroy(transform.gameObject, 3.0f);
        if (Gamemanager.instance != null)
            Gamemanager.instance.TriggerWinningState();
    }
    void Reset_Attack()
    {
        attack_count = 0;
    }
    void Update()
    {
        //SetHp(); /* Handle by Character.cs*/
        if (!CanMove)
        {
            agent.ResetPath();
        }

        //Debug.Log("Attack Count" + attack_count);
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
                //Receivedam();
            }
        }
        if (!space)
        {
            IsAttack = true;
        }

        /* HP is handled by Character.cs */
        //if (Hp < 0)
        //{
        //    IsDead = true;
        //}

        /* The dead is handle by Character.cs */
        //if (IsDead)
        //{
        //    IsDead = false;
        //    Hp = 0f;
        //    CanMove = false;
        //    die();
        //}
    }
    public void OnSpace(InputAction.CallbackContext context)
    {
        space = context.performed;
    }
    //private void Receivedam()
    //{
    //    float damage = 6.0f;
    //    if (Hp > 0)
    //    {

    //        Hp -= damage;


    //    }

    //    IsAttack = false;
    //}
    private void set_property()
    {
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
                        meleeTrigger.gameObject.SetActive(true);
                    }
                    if (CanAttack2)
                    {
                        anim.SetTrigger("Attack2");
                        meleeTrigger.gameObject.SetActive(true);
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