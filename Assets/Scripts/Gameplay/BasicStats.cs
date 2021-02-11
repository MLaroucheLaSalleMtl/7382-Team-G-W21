using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats
{
    Strength,
    Agility,
    Intelligence,
    Fortitude
}

[System.Serializable]
public class BasicStats
{
    private Stats _stats;
    private float _points;
    public float Points { get { return _points; } set { _points = value; } }

    public BasicStats(Stats stat, float points)
    {
        _stats = stat;
        _points = points;
    }
}
