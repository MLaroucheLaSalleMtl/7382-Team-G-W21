using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Function to set the start button behavior
    /// </summary>
    public void OnBtnStartPressed()
    {
        SceneController.instance.NextScene = "TestScene";
        SceneController.instance.LoadScene("LoadingScene");
        //SceneController.instance.LoadSceneAsync("TestScene");
    }
}
