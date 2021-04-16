using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilmentsManager : MonoBehaviour
{
    public static AilmentsManager instance;

    [Header("Hunger variables")]
    public float hungerLevel;
    public float hungerRate;
    private float _hungerMax;

    private void Awake()
    {
        instance = this;

        InitAilments();
    }

    private void FixedUpdate()
    {
        HungerState();
    }

    private void HungerState()
    {
        if(hungerLevel > 0)
            hungerLevel -= Time.fixedDeltaTime * hungerRate;

        if (PlayerGUI.instance != null)
            PlayerGUI.instance.UpdateHunger(hungerLevel, _hungerMax);

        if (hungerLevel <= 0)
            Gamemanager.instance.characterCtrl.Character.ReceiveDamage(2);
    }

    public void InitAilments()
    {
        hungerLevel = 100;
        _hungerMax = hungerLevel;
    }

    //copy这个方法来做hp stamina
    public void RecoveryHunger(float food)
    {
        //Debug.LogError(_hungerMax);
        hungerLevel += food;
        hungerLevel = hungerLevel >= _hungerMax ? _hungerMax : hungerLevel;// to limit the maximum value to 100
        //Debug.LogError(_hungerMax);
    }
}
