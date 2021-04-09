using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gamemanager : MonoBehaviour
{
    private bool PressESC;
    [SerializeField] private GameObject Stop_Menu;

    void Start()
    {
        PressESC = false;
        Stop_Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PressESC)
        {
            Stop_Menu.SetActive(true);
        }
    }
    public void PressedESC()
    {
        PressESC = true;
    }
}
