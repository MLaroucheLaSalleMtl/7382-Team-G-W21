using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gamemanager : MonoBehaviour
{
    private bool PressESC;
    private bool In_menu;
    [SerializeField] private GameObject Stop_Menu;

    // Different spawn points for the character
    public Transform[] spawnPoints;
    public GameObject player;
    public CharacterCtrl characterCtrl;

    public static Gamemanager instance;

    private void Awake()
    {
        instance = this;

        characterCtrl = player.GetComponent<CharacterCtrl>();
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
        if (characterCtrl.Character.IsDead) return;

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

        int randomPos = Random.Range(0, spawnPoints.Length);
        player.transform.position = spawnPoints[randomPos].position;

        yield return new WaitForSeconds(3f);
        LoadingPanelManager.instance.Hide();
    }

    public bool IsPlayerDead()
    {
        return characterCtrl.Character.IsDead;
    }

    public void TriggerWinningState()
    {
        StartCoroutine(WaitForWin());
    }

    IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(5f);
        WinPanelManager.instance.Show();
    }
}
