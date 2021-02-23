using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp;
    private float max_hp;
    private float mp;
    private float damage;
    private float move_speed;

    public float Hp { get => hp; set => hp = value; }
    public float Mp { get => mp; set => mp = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Move_speed { get => move_speed; set => move_speed = value; }
    public float Max_hp { get => max_hp; set => max_hp = value; }

    void Start()
    {

    }
    public void Set_Enemy()
    {
        Max_hp = Hp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
