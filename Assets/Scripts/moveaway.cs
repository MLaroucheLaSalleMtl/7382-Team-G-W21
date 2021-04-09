using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveaway : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject Player;
    public float distancetorun = 4.0f;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        //Debug.Log("distance" + distance);
        if (distance < distancetorun)
        {
            Vector3 dirtorun = transform.position - Player.transform.position;
            Vector3 newpods = transform.position + dirtorun;
            agent.SetDestination(newpods);
        }
        animMove();
    }
    private void animMove()
    {
        float maginitude;
        maginitude = agent.velocity.magnitude;
        anim.SetFloat("Magnitude", maginitude);
    }
}
