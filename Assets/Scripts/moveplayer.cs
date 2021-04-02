using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class moveplayer : MonoBehaviour
{
    float X;
    float Y;
    [SerializeField] private float speed;
    CharacterController mycharacter;

    // Start is called before the first frame update
    void Start()
    {
        mycharacter = GetComponent<CharacterController>();
    }
    public void Onmove(InputAction.CallbackContext context)
    {
        Vector2 Input = context.ReadValue<Vector2>();
        X = Input.x;
        Y = Input.y;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = (Vector3.left * Y + Vector3.forward * X) * Time.deltaTime;
        velocity *= speed;
        mycharacter.Move(velocity);
    }
}
