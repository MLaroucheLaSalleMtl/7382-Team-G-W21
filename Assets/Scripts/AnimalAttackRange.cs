using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAttackRange : MonoBehaviour
{
    private bool InAttackRange;

    public bool InAttackRange1 { get => InAttackRange; set => InAttackRange = value; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InAttackRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InAttackRange1 = false;
        }
    }
}
