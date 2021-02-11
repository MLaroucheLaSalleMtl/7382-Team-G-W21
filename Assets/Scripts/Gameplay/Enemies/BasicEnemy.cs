using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _character.Init(10, 10, 10, 10);
    }
}
