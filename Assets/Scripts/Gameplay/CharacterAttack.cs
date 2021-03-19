using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : CharacterAction
{
    public float attackCD;
    public bool isWeaponEquipped;
    private bool _isAttacking;
    private bool _isEquipping;
    [SerializeField] private ActionTrigger _actionTrigger;

    private CharacterCtrl _characterCtrl;

    public GameObject swordPrefab, shieldPrefab;

    public static CharacterAttack instance;

    public int combo;
    float lastActionTime = 0;
    float comboDelay = 0.5f;

    private void Awake()
    {
        instance = this;
        _characterCtrl = GetComponent<CharacterCtrl>();
    }

    private void Start()
    {
        _actionTrigger.actionFeedback.AddListener(_characterCtrl.Character.DoDamage);
    }

    private void Update()
    {
        if (Time.time - lastActionTime > comboDelay)
        {
            combo = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!_characterCtrl.isMoving)
        {
            if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("IdleBT") && combo > 0)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 1);
            }
            else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01_SwordShield") && combo > 1)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 2);
            }
            else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02_SwordShield") && combo > 2)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 3);
            }
            else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03_SwordShield") && combo > 3)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 4);
            }
            else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04_SwordShield") && combo > 4)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 5);
            }
        }
    }

    /// <summary>
    /// Input OnAttack event
    /// </summary>
    /// <param name="context"></param>
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_characterCtrl.isMoving && isWeaponEquipped)
            {
                lastActionTime = Time.time;
                combo++;

                combo = Mathf.Clamp(combo, 0, 5);
            }
        }
    }

    public void OnEquip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_isEquipping)
            {
                _isEquipping = true;
                if (!isWeaponEquipped)
                {
                    _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 1);
                    _characterCtrl.anim.SetBool("EquipWeapon", true);
                    StartCoroutine(EquipFinishRoutine());
                }
                else
                {
                    _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 1);
                    _characterCtrl.anim.SetBool("EquipWeapon", false);
                    StartCoroutine(UnequipFinishRoutine());
                }
            }
        }
    }

    public void TriggerAttack()
    {
        _characterCtrl.SpendStamina(2, () => { _actionTrigger.gameObject.SetActive(true); });
    }

    public void StopTriggerAttack()
    {
        _actionTrigger.gameObject.SetActive(false);
    }

    /// <summary>
    /// Attack delay
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackRoutine()
    {
        //_characterCtrl.anim.SetTrigger("Attack");
        //_characterCtrl.anim.SetInteger("Sword_Attack", 1);
        _isAttacking = true;
        _actionTrigger.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _actionTrigger.gameObject.SetActive(false);
        yield return new WaitForSeconds(attackCD - 0.1f);
        _isAttacking = false;
    }


    IEnumerator EquipFinishRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        swordPrefab.SetActive(true);
        shieldPrefab.SetActive(true);

        yield return new WaitForSeconds(0.8f);

        _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 0);

        yield return new WaitForSeconds(0.1f);

        isWeaponEquipped = true;
        DOTween.To(SetAnimation, 0, 1, 0.5f);
        _isEquipping = false;
    }

    void SetAnimation(float value)
    {
        _characterCtrl.anim.SetFloat("CharacterState", value);
    }

    IEnumerator UnequipFinishRoutine()
    {
        yield return new WaitForSeconds(0.35f);

        swordPrefab.SetActive(false);
        shieldPrefab.SetActive(false);

        yield return new WaitForSeconds(0.7f);

        _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 0);

        yield return new WaitForSeconds(0.1f);

        isWeaponEquipped = false;
        DOTween.To(SetAnimation, 1, 0, 0.5f);
        _isEquipping = false;
    }
}
