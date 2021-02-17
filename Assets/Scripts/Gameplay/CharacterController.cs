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
        _character.Init(statsScriptable.basicStats, 1, statsScriptable.BaseHP, statsScriptable.BaseMana, statsScriptable.BaseEnergy, statsScriptable.BaseDefense);
    }

    private void FixedUpdate()
    {
        RecoverEnergy();
    }

    /// <summary>
    /// Function to recover energy over time
    /// </summary>
    private void RecoverEnergy()
    {
        if (!_isAction && _character.energy < 100)
        {
            _character.energy += Time.deltaTime * recoverySpeed;
            if (_character.energy > _character.MaxEnergy)
                _character.energy = _character.MaxEnergy;
        }
    }

    /// <summary>
    /// Function to spend energy and do the action related to that
    /// </summary>
    /// <param name="energy">Amount of energy to spend</param>
    /// <param name="action">Action feedback</param>
    public void SpendEnergy(float energy, UnityAction action, UnityAction fail = null)
    {
        float remain = _character.energy - energy;

        if (remain < 0)
        {
            if (fail != null)
                fail.Invoke();
            return;
        }

        _character.energy = remain;

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
