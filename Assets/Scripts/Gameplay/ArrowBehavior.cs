using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigid;

    public float moveSpeed;

    private Character _owner;

    public void InitArrow(Vector3 direction, Character owner)
    {
        _owner = owner;
        _rigid.AddForce(direction * moveSpeed, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Character enemy = other.GetComponent<Character>();
            if (enemy != null)
            {
                _owner.DoDamage(enemy);                
                Destroy(gameObject);
            }
        }
    }
}
