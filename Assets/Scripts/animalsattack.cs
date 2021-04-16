using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// this enum by iris
public enum AnimamalType
{
    deer,
    wolf,
    bear,
}
public class animalsattack : MonoBehaviour
{
    [SerializeField]
    AnimamalType animamalType;
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
    private AnimatorStateInfo info;
    private bool IfDead;
    private AudioSource[] aniamlsaudio;
    public float HP1 { get => HP; set => HP = value; }
    public float Max_HP1 { get => Max_HP; set => Max_HP = value; }

    // Variables to handle the character stats
    public StatsScriptable statsScriptable;
    private Character _character;

    // Variable feedback for action trigger
    [SerializeField] private ActionTrigger _actionTrigger;

    void Start()
    {
        Max_HP1 = HP1;
        agent = GetComponent<NavMeshAgent>();
        CanMove = true;
        Anim = GetComponent<Animator>();
        AttackTimer = AttackRate;
        CanAttack = true;
        //IfDead = false; /* Handle by Character */
        aniamlsaudio = gameObject.GetComponents<AudioSource>();

        // Initialize the character
        _character = GetComponent<Character>();
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseStamina, statsScriptable.BaseDefense);

        _actionTrigger.actionFeedback.AddListener(_character.DoDamage);
    }


    /* Function not needed since Character handles the HP */
    private void SetHP()
    {
        HPImage.fillAmount = HP1 / Max_HP1;
        HPText.text = HP1 + "/" + Max_HP1;

    }
  //From line 65 to to 98 by iris
    private void Dead()
    {
        //if (HP1 <= 0f && !IfDead)
        //{
        //    CanMove = false;
        //    Anim.SetTrigger("IsDead");
        //    IfDead = true;
        //    Destroy(this.gameObject, 3f);
        //    switch (animamalType)
        //    {
        //        case AnimamalType.deer:
        //            DropMaterial("deerMeat");

        //            break;
        //        case AnimamalType.wolf:
        //            DropMaterial("wolfMeat");
        //            TaskManager.GetInstance().isKill = true;
        //            break;
        //        case AnimamalType.bear:
        //            DropMaterial("bearMeat");

        //            break;
        //        default:
        //            break;
        //    }
        //}

        // No need to do the check since the Character.cs handles it
        CanMove = false;
        Anim.SetTrigger("IsDead");
        Destroy(this.gameObject, 3f);
        switch (animamalType)
        {
            case AnimamalType.deer:
                DropMaterial("deerMeat");
                break;
            case AnimamalType.wolf:
                DropMaterial("wolfMeat");
                if (TaskManager.GetInstance() != null)
                    TaskManager.GetInstance().isKill = true;
                break;
            case AnimamalType.bear:
                DropMaterial("bearMeat");
                break;
            default:
                break;
        }
    }

    public void DropMaterial(string MaterialName)
    {
        var m = Instantiate(Resources.Load<GameObject>("Item Prefab/" + MaterialName));
        m.transform.position = transform.position;
        m.name = "MaterialName";
    }
    // Update is called once per frame
    void Update()
    {
        if (_character.IsDead)
            return;

        //Dead(); /* This is handle by Character.cs */
        //SetHP();   /* This is handle by Character.cs */
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
        if (aniamlsaudio.Length > 0)
            aniamlsaudio[1].Play();

        Invoke("ResetCanMove", TimesAfterAttack);

        // Calling the coroutine to apply the trigger logic
        StartCoroutine(AttackRoutine());
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

    /// <summary>
    /// Coroutine to handle the action trigger
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackRoutine()
    {
        _actionTrigger.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _actionTrigger.gameObject.SetActive(false);
    }
}
