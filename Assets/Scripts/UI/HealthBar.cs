using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI txtHP;
    public TextMeshProUGUI txtMaxHP;
    public Slider mySlider;

    public void Init(float currentValue, float maxValue)
    {
        mySlider.maxValue = maxValue;
        mySlider.value = currentValue;
        txtHP.text = currentValue.ToString();
        txtMaxHP.text = maxValue.ToString();
    }

    public void UpdateBar(float value)
    {
        mySlider.value = value;
        txtHP.text = value.ToString();
    }
}
