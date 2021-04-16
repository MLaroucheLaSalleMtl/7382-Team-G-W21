using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelManager : MonoBehaviour
{
    public static WinPanelManager instance;

    public GameObject contents;

    private void Awake()
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
}
