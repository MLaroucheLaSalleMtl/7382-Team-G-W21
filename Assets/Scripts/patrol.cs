using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class patrol : MonoBehaviour
{
    public GameObject[] go;
    private List<Vector3> wayPoints = new List<Vector3>();
    [SerializeField] private int index;
    private Vector3 destination;
    [SerializeField] private int[] num = new int[7];
    [SerializeField] private bool isRandom;
    NavMeshAgent agent;
    // Start is called before the first frame update
    private void Awake()
    {
        index = 0;
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        LoadPath(go);
        isRandom = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnermyPatrol();
    }
    private void LoadPath(GameObject[] go)
    {
        index = 0;
        ResetPatrol();
        wayPoints.Clear();//clear list after finish patrol
        for (int i = 0; i < go.Length; i++)
        {
            wayPoints.Add(go[i].transform.position);
        }
    }

    private void EnermyPatrol()
    {
        //when the waypoints reached to the end, reset the patrol
        if (index == go.Length)
        {
            LoadPath(go);
        }
        //go to the destination
        if (!isRandom)
        {
            isRandom = true;
            destination = wayPoints[RandPos()];
            agent.destination = destination;
        }

        //the enermy will not reach the same position
        //so I create a tiny gap
        if (Vector3.Distance(this.transform.position, destination) <= 0.5f)
        {
            //if enermy reach the patrol position, increment index

            index++;
            isRandom = false;
            Debug.Log(isRandom);
        }
    }

    private int RandPos()
    {
         int randomPos = Random.Range(0, 7);
        //Debug.Log(randomPos);
        if (num[randomPos] != -1)
        {
            RandPos();
        }
        num[randomPos] = randomPos;
        return randomPos;
    }

    private void ResetPatrol()
    {
        for (int i = 0; i < num.Length; i++)
        {
            num[i] = -1;
        }
    }
}
