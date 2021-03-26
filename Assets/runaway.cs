using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class runaway : MonoBehaviour
{
    public Transform Target;
    private Vector3 Offset;   //the distance between this gameobject with target.
    private NavMeshAgent Agent;
    private bool IsRunAway;
    void Start()
    {
        Agent = new NavMeshAgent();
        Offset = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Offset.x = transform.position.x - other.transform.position.x;
            Offset.y = transform.position.y - other.transform.position.y;
            Agent.destination = -Offset;
        }
    }
}