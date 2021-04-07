using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    public Image health;
    public Image hunger;
    public Image stamina;

    public static PlayerGUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(float HP, float maxHP)
    {
        this.health.fillAmount = HP / maxHP;
    }

    public void UpdateStamina(float stamina, float maxStamina)
    {
        this.stamina.fillAmount = stamina / maxStamina;
    }

    public void UpdateHunger(float hunger, float maxHunger)
    {
        this.hunger.fillAmount = hunger / maxHunger;
    }
}
