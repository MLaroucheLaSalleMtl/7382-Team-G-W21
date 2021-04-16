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

    // This variables are for Sixiang's method in the HP bar implementation
    public Image hpSlide;
    public Text hpTxt;
    private float _maxHP;

    /// <summary>
    /// Function to initialize the variables
    /// </summary>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    public void Init(float currentValue, float maxValue)
    {
        if (mySlider != null)
        {
            mySlider.maxValue = maxValue;
            mySlider.value = currentValue;
        }

        if (txtHP != null)
            txtHP.text = currentValue.ToString();

        if (txtMaxHP != null)
            txtMaxHP.text = maxValue.ToString();

        if (hpSlide != null)
        {
            hpSlide.fillAmount = 1;
            _maxHP = maxValue;
        }


        if (hpTxt != null)
            hpTxt.text = currentValue + "/" + maxValue;
    }

    /// <summary>
    /// Function to update the HP bar UI
    /// </summary>
    /// <param name="value"></param>
    public void UpdateBar(float value)
    {
        if (mySlider != null)
            mySlider.value = value;

        if (txtHP != null)
            txtHP.text = value.ToString();

        if (hpSlide != null)
            hpSlide.fillAmount = value / _maxHP;

        if (hpTxt != null)
        {
            value = value < 0 ? 0 : value;
            hpTxt.text = value + "/" + _maxHP;
        }            
    }
}
