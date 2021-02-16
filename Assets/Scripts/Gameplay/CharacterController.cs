using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Character _character;

    public StatsScriptable statsScriptable;

    // Start is called before the first frame update
    void Awake()
    {
        _character = GetComponent<Character>();
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseEnergy, statsScriptable.BaseDefense);
    }
}
