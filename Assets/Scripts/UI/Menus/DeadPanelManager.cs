using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanelManager : MonoBehaviour
{
    public static DeadPanelManager instance;

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
