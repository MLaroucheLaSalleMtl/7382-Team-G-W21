using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public void OnBtnBackMainMenuPressed()
    {
        SceneController.instance.LoadScene("Main menu");
    }
}
