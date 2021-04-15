using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gamemanager : MonoBehaviour
{
    private bool PressESC;
    private bool In_menu;
    [SerializeField] private GameObject Stop_Menu;

    private void Awake()
    {
        // Controlling the loading panel at starting scene
        StartCoroutine(LoadingRoutine());
    }

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

    /// <summary>
    /// Function coroutine to set the loading panel
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadingRoutine()
    {
        LoadingPanelManager.instance.Show();
        LoadingPanelManager.instance.HideSlider();
        LoadingPanelManager.instance.SetContentText("Loading things you can play with");
        yield return new WaitForSeconds(3f);
        LoadingPanelManager.instance.Hide();
    }
}
