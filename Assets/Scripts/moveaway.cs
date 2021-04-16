using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class moveaway : MonoBehaviour
{
    [SerializeField]
    AnimamalType animamalType;

    [SerializeField] private Image HPImage;
    [SerializeField] private Text HPText;
    private NavMeshAgent agent;
    public GameObject Player;
    public float distancetorun = 4.0f;
    private Animator Anim;
    [SerializeField] private float HP;
    private float Max_HP;
    private bool isDEAD;
    public float HP1 { get => HP; set => HP = value; }
    public float Max_HP1 { get => Max_HP; set => Max_HP = value; }

    // Variables to handle the character stats
    public StatsScriptable statsScriptable;
    private Character _character;

    // Start is called before the first frame update
    void Start()
    {
        Max_HP1 = HP1;
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        //isDEAD = false; /* This is handle in Character.cs */

        // Initialize the character
        _character = GetComponent<Character>();
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseStamina, statsScriptable.BaseDefense);
    }

    // Update is called once per frame
    void Update()
    {
        //SetHP(); /* Handle by Character.cs */
        //if (HP1 <= 0 && !isDEAD)
        //{
        //    Anim.SetTrigger("IsDead");
        //    isDEAD = true;
        //    Destroy(this.gameObject, 3f);
        //}
        float Magnitude = agent.velocity.magnitude;
        Anim.SetFloat("Magnitude", Magnitude);

        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance < distancetorun)
        {
            Vector3 dirtorun = transform.position - Player.transform.position;
            Vector3 newpods = transform.position + dirtorun;
            agent.SetDestination(newpods);
        }
    }

    private void SetHP()
    {
        HPImage.fillAmount = HP1 / Max_HP1;
        HPText.text = HP1 + "/" + Max_HP1;

    }

    public void OnDeath()
    {
        Anim.SetTrigger("IsDead");
        //isDEAD = true; /* This is handle in Character.cs */
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
}
