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
    public Stats stat;
    public float points;

    public BasicStats(Stats stat, float points)
    {
        this.stat = stat;
        this.points = points;
    }
}
