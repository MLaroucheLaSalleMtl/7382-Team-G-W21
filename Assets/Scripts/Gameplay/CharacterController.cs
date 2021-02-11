using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Character _character;

    // Start is called before the first frame update
    void Awake()
    {
        _character = GetComponent<Character>();
        _character.Init(10, 10, 10, 10);
        _character.damage = 2;
    }
}
