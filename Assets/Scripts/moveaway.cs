using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveaway : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject Player;
    public float distancetorun = 4.0f;
    private Animator Anim;
    [SerializeField] private float hp;
    private bool isDEAD;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        isDEAD = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !isDEAD)
        {
            Anim.SetTrigger("IsDead");
        }
        float Magnitude = agent.velocity.magnitude;
        Anim.SetFloat("Magnitude", Magnitude);

        float distance = Vector3.Distance(transform.position, Player.transform.position);
        //Debug.Log("distance" + distance);
        if (distance < distancetorun)
        {
            Vector3 dirtorun = transform.position - Player.transform.position;
            Vector3 newpods = transform.position + dirtorun;
            agent.SetDestination(newpods);
        }
    }
}
