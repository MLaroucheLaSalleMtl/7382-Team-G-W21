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
    public GameObject globaltips;
    private float maxHunger = 100;
    private float maxHealth = 100;
    private float maxStamina = 100;
    public static PlayerGUI instance;

    private void Awake()
    {
        instance = this;
    }

    float tempHunger = 2;
    float curHunger = 100;
    float curHealth = 100;
    float curStamina = 100;
    private void Update()
    {
        tempHunger -= Time.deltaTime;
        if (tempHunger<=0)
        {
            tempHunger = 2;
            curHunger -= 2;
            if (curHunger <= 0)
            {
                curHunger = 0;
                tips.SetActive(true);
                tips.GetComponentInChildren<Text>().text = "Game Over!";
            }
            //maxHunger = maxHunger <= 0 ? 0 : maxHunger;
        }
        UpdateHunger();
        //刷新health 和stamina值
        //UpdateHealth();
        UpdateStamina(curStamina);
    }
    public void UpdateHealth( )
    {
        this.health.fillAmount = curHealth / maxHealth;
    }

    public void UpdateStamina(float stamina)
    {
        this.stamina.fillAmount = stamina / maxStamina;
    }

    public void UpdateHunger()
    {
        this.hunger.fillAmount =curHunger / maxHunger;
    }

    //copy这个方法来做hp stamina
    public void RecoveryHunger(float food)
    {
        Debug.LogError(curHunger);
        curHunger += food;
        curHunger = curHunger >= 100 ? 100 : curHunger;// to limit the maximum value to 100
        Debug.LogError(curHunger);
    }

    public void UpdateHealth(float HP, float maxHP)
    {
        this.health.fillAmount = HP / maxHP;
    }

    public void UpdateStamina(float stamina, float maxStamina)
    {
        this.stamina.fillAmount = stamina / maxStamina;
    }
    public void Setglobaltips(string content)
    {
        globaltips.SetActive(true);
        globaltips.GetComponentInChildren<Text>().text = content;
    }
}
