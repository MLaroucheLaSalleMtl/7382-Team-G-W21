using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum WeaponEquipped
{
    BOW,
    SWORD,
    NONE
}

public class CharacterAttack : CharacterAction
{
    public float attackCD;
    public bool isWeaponEquipped;
    private bool _isAttacking;
    private bool _isEquipping;
    [SerializeField] private ActionTrigger _actionTrigger;

    private CharacterCtrl _characterCtrl;

    public GameObject swordPrefab, shieldPrefab;
    public GameObject bowPrefab, arrowPrefab;

    public GameObject shottingArrowPrefab;
    public Transform shootingPoint;

    public static CharacterAttack instance;

    public int meleeCombo;
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
            meleeCombo = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!_characterCtrl.isMoving)
        {
            if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("IdleBT") && meleeCombo > 0)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 1);
            }
            else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01_SwordShield") && meleeCombo > 1)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 2);
            }
            else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02_SwordShield") && meleeCombo > 2)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 3);
            }
            else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03_SwordShield") && meleeCombo > 3)
            {
                _characterCtrl.anim.SetTrigger("Attack");
                _characterCtrl.anim.SetInteger("Sword_Attack", 4);
            }
            else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04_SwordShield") && meleeCombo > 4)
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
                if (_characterCtrl.weaponEquipped == WeaponEquipped.SWORD)
                {
                    lastActionTime = Time.time;
                    meleeCombo++;
                    meleeCombo = Mathf.Clamp(meleeCombo, 0, 5);
                }
                else if (_characterCtrl.weaponEquipped == WeaponEquipped.BOW)
                {
                    _characterCtrl.anim.SetTrigger("Bow_Attack");                    
                    GameObject arrow = Instantiate(shottingArrowPrefab, shootingPoint.position, shootingPoint.rotation);
                    arrow.GetComponent<ArrowBehavior>().InitArrow(shootingPoint.forward);
                }
            }
        }
    }

    public void OnPrepareAction(InputAction.CallbackContext context)
    {

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
                    _characterCtrl.weaponEquipped = WeaponEquipped.SWORD;
                    _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 1);
                    _characterCtrl.anim.SetBool("EquipWeapon", true);
                    StartCoroutine(EquipFinishRoutine(0));
                }
                else
                {
                    _characterCtrl.isAiming = false;
                    _characterCtrl.weaponEquipped = WeaponEquipped.NONE;
                    _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 1);
                    _characterCtrl.anim.SetBool("EquipWeapon", false);
                    _characterCtrl.anim.SetInteger("WeaponID", 0);
                    StartCoroutine(UnequipFinishRoutine(0));
                }
            }
        }
    }

    public void OnEquip2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_isEquipping)
            {
                _isEquipping = true;
                if (!isWeaponEquipped)
                {
                    //_characterCtrl.isAiming = true;
                    _characterCtrl.weaponEquipped = WeaponEquipped.BOW;
                    _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 1);
                    _characterCtrl.anim.SetBool("EquipWeapon", true);
                    StartCoroutine(EquipFinishRoutine(1));
                }
                else
                {
                    _characterCtrl.isAiming = false;
                    _characterCtrl.weaponEquipped = WeaponEquipped.NONE;
                    _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 1);
                    _characterCtrl.anim.SetBool("EquipWeapon", false);
                    _characterCtrl.anim.SetInteger("WeaponID", 1);
                    StartCoroutine(UnequipFinishRoutine(1));
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

    IEnumerator EquipFinishRoutine(float weaponState)
    {
        yield return new WaitForSeconds(0.5f);

        //swordPrefab.SetActive(true);
        //shieldPrefab.SetActive(true);
        WeaponStates();

        yield return new WaitForSeconds(0.8f);

        _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 0);

        yield return new WaitForSeconds(0.1f);

        isWeaponEquipped = true;
        DOTween.To(x => SetAnimation(x, weaponState), 0, 1, 0.5f);
        _isEquipping = false;
    }

    IEnumerator UnequipFinishRoutine(float weaponState)
    {
        yield return new WaitForSeconds(0.35f);

        //swordPrefab.SetActive(false);
        //shieldPrefab.SetActive(false);
        WeaponStates();

        yield return new WaitForSeconds(0.7f);

        _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 0);

        yield return new WaitForSeconds(0.1f);

        isWeaponEquipped = false;
        DOTween.To(x => SetAnimation(x, weaponState), 1, 0, 0.5f);
        _isEquipping = false;
    }

    /// <summary>
    /// Function to set the animation states depending of the weapon equipped
    /// </summary>
    /// <param name="CharacterState"></param>
    /// <param name="weaponState"></param>
    void SetAnimation(float CharacterState, float weaponState)
    {
        _characterCtrl.anim.SetFloat("WeaponState", weaponState);
        _characterCtrl.anim.SetFloat("CharacterState", CharacterState);
    }

    private void WeaponStates()
    {
        switch (_characterCtrl.weaponEquipped)
        {
            case WeaponEquipped.SWORD:
                swordPrefab.SetActive(true);
                shieldPrefab.SetActive(true);
                bowPrefab.SetActive(false);
                arrowPrefab.SetActive(false);
                break;
            case WeaponEquipped.BOW:
                swordPrefab.SetActive(false);
                shieldPrefab.SetActive(false);
                bowPrefab.SetActive(true);
                arrowPrefab.SetActive(true);
                break;
            case WeaponEquipped.NONE:
                swordPrefab.SetActive(false);
                shieldPrefab.SetActive(false);
                bowPrefab.SetActive(false);
                arrowPrefab.SetActive(false);
                break;
            default:
                break;
        }
    }
}
