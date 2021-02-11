using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public LayerMask layerMask;
    private void OnTriggerEnter(Collider other)
    {
        if(1 << other.gameObject.layer == layerMask)
        {
            Debug.Log("ENEMY! DANGER! DANGER!");
        }
    }
}
