using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class deerhp : MonoBehaviour
{
    [SerializeField] private Image HPImage;
    [SerializeField] private Text HPText;
    [SerializeField] private float HP;
    NavMeshAgent agent;
    private float Max_HP;
    private bool CanMove;
    private Animator Anim;
    private bool IfDead;
    public float HP1 { get => HP; set => HP = value; }
    public float Max_HP1 { get => Max_HP; set => Max_HP = value; }
    // Start is called before the first frame update
    void Start()
    {
        Max_HP1 = HP1;
        CanMove = true;
        Anim = GetComponent<Animator>();
        IfDead = false;
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        Dead();
        SetHP();
        if (!CanMove)
        {
            agent.ResetPath();
        }

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
}
