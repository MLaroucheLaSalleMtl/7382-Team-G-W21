using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class moveaway : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        Max_HP1 = HP1;
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        isDEAD = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetHP();
        if (HP1 <= 0 && !isDEAD)
        {
            Anim.SetTrigger("IsDead");
            isDEAD = true;
            Destroy(this.gameObject, 3f);
        }
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
}
