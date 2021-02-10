using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    public int healthMax;
    public int hungerMax;
    public int staminaMax;

    public Image health;
    public Image hunger;
    public Image stamina;

    public float healthIncrease;
    public float hungerDecrease;
    public float staminaIncrease;

    public float updatedHealth;
    public float updatedHunger;
    public float updatedStamina;

    // Start is called before the first frame update
    void Start()
    {
        healthIncrease = 5f;
        hungerDecrease = -1f;
        staminaIncrease = 10f;

        healthMax = 100;
        hungerMax = 100;
        staminaMax = 100;
    }

    // Update is called once per frame
    void Update()
    {
        updatedHealth += healthIncrease * Time.deltaTime;
        health.fillAmount = updatedHealth / healthMax;

        updatedHunger += hungerDecrease * Time.deltaTime;
        hunger.fillAmount = updatedHunger / hungerMax;

        updatedStamina += staminaIncrease * Time.deltaTime;
        stamina.fillAmount = updatedStamina / staminaMax;

        if(updatedHealth >= healthMax)
        {
            updatedHealth = healthMax;
        }

        if (updatedHunger >= hungerMax)
        {
            updatedHunger =hungerMax;
        }

        if (updatedStamina >= staminaMax)
        {
            updatedStamina = staminaMax;
        }
    }
}
