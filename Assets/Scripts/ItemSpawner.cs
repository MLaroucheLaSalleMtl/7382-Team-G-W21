using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform[] itemSpawners;

    public Transform itemParent;

    public GameObject[] itemsPrefabs;

    void Awake()
    {
        int quantity = itemsPrefabs.Length;
        for (int i = 0, length = itemSpawners.Length; i < length; i++)
        {
            int id = Random.Range(0, quantity);
            Transform itemSpawner = itemSpawners[i];
            if (Physics.Raycast(itemSpawner.position, itemSpawner.TransformDirection(Vector3.down), out RaycastHit hit, 100f))
            {
                // TODO::For next iteration, avoid the addition to the y component and make the positioning of the items 
                if (hit.collider.CompareTag("Floor"))
                {
                    Vector3 pos = hit.point;
                    pos.y += 10f;
                    Instantiate(itemsPrefabs[id], pos, Quaternion.identity, itemParent);
                }
            }
        }
    }
}
