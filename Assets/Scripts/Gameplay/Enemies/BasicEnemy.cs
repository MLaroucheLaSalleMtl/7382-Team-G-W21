using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Character _character;

    public StatsScriptable statsScriptable;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseStamina, statsScriptable.BaseDefense);
    }
}
