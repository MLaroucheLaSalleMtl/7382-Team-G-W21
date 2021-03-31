using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    public Image health;
    public Image hunger;
    public Image stamina;
    public GameObject tips;
    private float maxHunger = 100;
    public static PlayerGUI instance;

    private void Awake()
    {
        instance = this;
    }

    float tempHunger = 1;
    float curHunger = 100;
    private void Update()
    {
        tempHunger -= Time.deltaTime;
        if (tempHunger<=0)
        {
            tempHunger = 1;
            curHunger -= 5;
            if (curHunger <= 0)
            {
                curHunger = 0;
                tips.SetActive(true);
                tips.GetComponentInChildren<Text>().text = "GameOver!!";
            }

            //maxHunger = maxHunger <= 0 ? 0 : maxHunger;
            UpdateHunger(curHunger);
        }
    }
    public void UpdateHealth(float HP, float maxHP)
    {
        this.health.fillAmount = HP / maxHP;
    }

    public void UpdateStamina(float stamina, float maxStamina)
    {
        this.stamina.fillAmount = stamina / maxStamina;
    }

    public void UpdateHunger(float hunger)
    {
        this.hunger.fillAmount = hunger / maxHunger;
    }
}
