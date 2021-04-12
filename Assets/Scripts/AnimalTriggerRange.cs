using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTriggerRange : MonoBehaviour
{
    // Start is called before the first frame update
    private bool FindPlayer;

    public bool FindPlayer1 { get => FindPlayer; set => FindPlayer = value; }

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
            FindPlayer1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FindPlayer1 = false;
        }
    }
}
