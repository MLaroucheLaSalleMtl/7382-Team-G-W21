using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gamemanager : MonoBehaviour
{
    private bool PressESC;
    private bool In_menu;
    [SerializeField] private GameObject Stop_Menu;
    void Start()
    {
        PressESC = false;
        Stop_Menu.SetActive(false);
        In_menu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PressESC)
        {
            Stop_Menu.SetActive(true);
        }
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        PressESC = context.performed;
    }


}
