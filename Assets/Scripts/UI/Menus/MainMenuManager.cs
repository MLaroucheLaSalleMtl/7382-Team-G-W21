using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public GameObject contents;

    void Awake()
    {
        instance = this;
    }

    public void Show()
    {
        contents.SetActive(true);
    }

    public void Hide()
    {
        contents.SetActive(false);
    }

    public void OnBtnStartPressed()
    {
        SceneController.instance.NextScene = "TestScene";
        SceneController.instance.LoadScene("LoadingScene");
        //SceneController.instance.LoadSceneAsync("TestScene");
    }
}
