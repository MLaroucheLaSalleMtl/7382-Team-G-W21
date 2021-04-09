using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigid;

    public float moveSpeed;

    public void InitArrow(Vector3 direction)
    {
        _rigid.AddForce(direction * moveSpeed, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {            
            Debug.Log("Hitting an enemy");
            Destroy(gameObject);
        }
    }
}
