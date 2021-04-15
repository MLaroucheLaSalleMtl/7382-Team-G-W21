using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelManager : MonoBehaviour
{
    public static LoadingPanelManager instance;

    public GameObject contents;

    public Slider loadSlider;
    public TextMeshProUGUI contentTxt;

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

    public void UpdateSlider(float value)
    {
        loadSlider.value = value;
    }

    public void HideSlider()
    {
        loadSlider.gameObject.SetActive(false);
    }

    public void SetContentText(string text)
    {
        contentTxt.text = text;
    }
}
