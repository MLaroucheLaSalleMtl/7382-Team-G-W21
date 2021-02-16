using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/StatsScriptable", order = 1)]
public class StatsScriptable : ScriptableObject
{
    public float BaseHP;
    public float BaseMana;
    public float BaseEnergy;
    public float BaseDefense;
    public BasicStats[] basicStats;
}
