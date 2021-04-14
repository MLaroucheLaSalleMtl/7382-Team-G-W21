using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    public float lookSphereCastRadius; // This could be set in the basic enemies
    public float lookRange; // This could be set in the basic enemies
    public float attackRange; // This could be set in the basic enemies    
    public float searchingTurnSpeed; // This could be set in the basic enemies
    public float searchDuration; // This could be set in the basic enemies

    private bool _aiActive;    

    [HideInInspector] public BasicEnemy _enemy;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!_aiActive)
            return;

        currentState.UpdateState(this);
    }

    /// <summary>
    /// Initializing StateController
    /// </summary>
    /// <param name="aiActivation"></param>
    /// <param name="wayPoints"></param>
    /// <param name="enemy"></param>
    public void SetupAI(bool aiActivation, List<Transform> wayPoints, BasicEnemy enemy)
    {
        _enemy = enemy;
        wayPointList = wayPoints;
        _aiActive = aiActivation;
        if (_aiActive)
            navMeshAgent.enabled = true;
        else
            navMeshAgent.enabled = false;
    }

    /// <summary>
    /// Function to go to the next state
    /// </summary>
    /// <param name="nextState"></param>
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    /// <summary>
    /// Function to create a delay before starting a new state
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    /// <summary>
    /// Exit State
    /// </summary>
    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookSphereCastRadius);
    }
}
