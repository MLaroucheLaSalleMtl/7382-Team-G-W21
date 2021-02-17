using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    private Character _character;
    public Character Character { get { return _character; } }

    public float recoverySpeed;

    public StatsScriptable statsScriptable;

    private bool _isAction;
    IEnumerator _actionRoutine;

    // Start is called before the first frame update
    void Awake()
    {
        _character = GetComponent<Character>();
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseStamina, statsScriptable.BaseDefense);
        PlayerGUI.instance.UpdateHealth(_character.health, _character.MaxHealth);
    }

    private void FixedUpdate()
    {
        RecoverStamina();
    }

    /// <summary>
    /// Function to recover energy over time
    /// </summary>
    private void RecoverStamina()
    {
        if (!_isAction && _character.stamina < 100)
        {
            _character.stamina += Time.deltaTime * recoverySpeed;
            if (_character.stamina > _character.MaxStamina)
                _character.stamina = _character.MaxStamina;
            PlayerGUI.instance.UpdateStamina(_character.stamina, _character.MaxStamina);
        }
    }

    /// <summary>
    /// Function to spend energy and do the action related to that
    /// </summary>
    /// <param name="stamina">Amount of energy to spend</param>
    /// <param name="action">Action feedback</param>
    public void SpendStamina(float stamina, UnityAction action, UnityAction fail = null)
    {
        float remain = _character.stamina - stamina;

        if (remain < 0)
        {
            if (fail != null)
                fail.Invoke();
            return;
        }

        _character.stamina = remain;
        PlayerGUI.instance.UpdateStamina(_character.stamina, _character.MaxStamina);

        if (action != null)
            action.Invoke();

        _isAction = true;
        if (_actionRoutine != null)
            StopCoroutine(_actionRoutine);
        _actionRoutine = ActionRoutine();
        StartCoroutine(_actionRoutine);
    }

    IEnumerator ActionRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _isAction = false;
    }
}
