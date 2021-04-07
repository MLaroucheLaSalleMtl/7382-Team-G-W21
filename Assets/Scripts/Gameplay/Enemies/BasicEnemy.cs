using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Character _character;

    public StatsScriptable statsScriptable;

    public bool activeAI;
    public List<Transform> wayPoints;
    private StateController _stateController;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseStamina, statsScriptable.BaseDefense);

        _stateController = GetComponent<StateController>();
    }

    private void Start()
    {
        if (_stateController != null)
            _stateController.SetupAI(activeAI, wayPoints);
    }
}
