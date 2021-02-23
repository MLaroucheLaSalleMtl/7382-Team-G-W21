using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveaway : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject Player;
    public float distancetorun = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector3.Distance(transform.position, Player.transform.position);
        //Debug.Log("distance" + distance);
        //if (distance < distancetorun)
        //{
        //    Vector3 dirtorun = transform.position - Player.transform.position;
        //    Vector3 newpods = transform.position + dirtorun;
        //    agent.SetDestination(newpods);
        //}
    }

    private void FixedUpdate()
    {
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
