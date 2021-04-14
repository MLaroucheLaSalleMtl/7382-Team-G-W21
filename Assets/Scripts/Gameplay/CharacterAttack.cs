using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum WeaponEquipped
{
    BOW,
    SWORD,
    AXE,
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
    public GameObject axePrefab;

    public GameObject shottingArrowPrefab;
    public Transform shootingPoint;

    public static CharacterAttack instance;

    public int meleeCombo;
    private float _lastActionTime = 0;
    private const float COMBODELAY = 0.5f;

    private void Awake()
    {
        instance = this;
        _characterCtrl = GetComponent<CharacterCtrl>();
    }

    private void Start()
    {
        _actionTrigger.actionFeedback.AddListener(_characterCtrl.Character.DoDamage);

        if (WeaponManager.GetInstance() != null)
            Debug.Log("CurEquipment:" + WeaponManager.GetInstance().curEquipWeapon);
    }

    private void Update()
    {
        CheckComboTime();
    }

    private void FixedUpdate()
    {
        if (!_characterCtrl.isMoving)
        {
            ComboHandle();
        }
    }

    #region Attacking functions
    /// <summary>
    /// Function to check the combo time
    /// </summary>
    private void CheckComboTime()
    {
        if (Time.time - _lastActionTime > COMBODELAY)
        {
            meleeCombo = 0;
        }
    }

    /// <summary>
    /// Function to handle the combo
    /// </summary>
    private void ComboHandle()
    {
        if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("IdleBT") && meleeCombo > 0)
        {
            SetAttackAnimation(1);
        }
        else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01_SwordShield") && meleeCombo > 1)
        {
            SetAttackAnimation(2);
        }
        else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02_SwordShield") && meleeCombo > 2)
        {
            SetAttackAnimation(3);
        }
        else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03_SwordShield") && meleeCombo > 3)
        {
            SetAttackAnimation(4);
        }
        else if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04_SwordShield") && meleeCombo > 4)
        {
            SetAttackAnimation(5);
        }
    }

    /// <summary>
    /// Function to set the attack animation for the combo system
    /// </summary>
    /// <param name="id"></param>
    private void SetAttackAnimation(int id)
    {
        _characterCtrl.anim.SetTrigger("Attack");
        _characterCtrl.anim.SetInteger("Sword_Attack", id);
    }

    /// <summary>
    /// Function to handle the player's attack
    /// </summary>
    private void HandleAttack()
    {
        if (!_characterCtrl.isMoving && isWeaponEquipped)
        {
            if (_characterCtrl.weaponEquipped == WeaponEquipped.SWORD || _characterCtrl.weaponEquipped == WeaponEquipped.AXE)
            {
                _lastActionTime = Time.time;
                meleeCombo++;
                meleeCombo = Mathf.Clamp(meleeCombo, 0, 5);
            }
            else if (_characterCtrl.weaponEquipped == WeaponEquipped.BOW)
            {
                if (_characterCtrl.anim.GetCurrentAnimatorStateInfo(2).IsName("Attack01Start_Bow"))
                {
                    _characterCtrl.anim.SetTrigger("Bow_Attack");
                }
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
            HandleAttack();
        }
    }
    #endregion

    #region Input system behavior for equipping - unequipping the weapons
    /// <summary>
    /// Input on equip 1
    /// </summary>
    /// <param name="context"></param>
    public void OnEquip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_isEquipping)
            {
                if (!_characterCtrl.IsDebugging) // Creating a debugging bool to test this feature in a scene without the WeaponManager
                {
                    if (WeaponManager.GetInstance().curEquipWeapon == WeaponType.Sword)
                    {
                        _isEquipping = true;
                        if (!isWeaponEquipped)
                        {
                            EquipWeapon(WeaponEquipped.SWORD, 0);
                        }
                        else
                        {
                            Unequipping(0, 0);
                        }
                    }
                    else
                    {
                        GameObject.Find("Player GUI").GetComponent<PlayerGUI>().Setglobaltips("curWeapon is not crafted");
                    }
                }
                else
                {
                    _isEquipping = true;
                    if (!isWeaponEquipped)
                    {
                        EquipWeapon(WeaponEquipped.SWORD, 0);
                    }
                    else
                    {
                        Unequipping(0, 0);
                    }
                }
            }
        }
    }

    /// <summary>
    /// On equip2, related to the bow
    /// </summary>
    /// <param name="context"></param>
    public void OnEquip2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_isEquipping)
            {
                if (!_characterCtrl.IsDebugging) // Creating a debugging bool to test this feature in a scene without the WeaponManager
                {
                    if (WeaponManager.GetInstance().curEquipWeapon == WeaponType.Bow)
                    {
                        _isEquipping = true;
                        if (!isWeaponEquipped)
                        {
                            EquipWeapon(WeaponEquipped.BOW, 1);                            
                        }
                        else
                        {
                            Unequipping(1, 1);
                        }
                    }
                    else
                    {
                        GameObject.Find("Player GUI").GetComponent<PlayerGUI>().Setglobaltips("curWeapon is not crafted");
                    }
                }
                else
                {
                    _isEquipping = true;
                    if (!isWeaponEquipped)
                    {
                        EquipWeapon(WeaponEquipped.BOW, 1);                        
                    }
                    else
                    {
                        Unequipping(1, 1);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Equip input for the axe
    /// </summary>
    /// <param name="context"></param>
    public void OnEquip3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_isEquipping)
            {
                if (!_characterCtrl.IsDebugging)
                {
                    if (WeaponManager.GetInstance().curEquipWeapon == WeaponType.Axe)
                    {
                        _isEquipping = true;
                        if (!isWeaponEquipped)
                        {
                            EquipWeapon(WeaponEquipped.AXE, 0);                            
                        }
                        else
                        {
                            Unequipping(0, 0);
                        }
                    }
                    else
                    {
                        GameObject.Find("Player GUI").GetComponent<PlayerGUI>().Setglobaltips("curWeapon is not crafted");
                    }
                }
                else
                {
                    _isEquipping = true;
                    if (!isWeaponEquipped)
                    {
                        EquipWeapon(WeaponEquipped.AXE, 0);                        
                    }
                    else
                    {
                        Unequipping(0, 0);
                    }
                }
            }
        }
    }
    #endregion

    #region Functions to set the animation events
    /// <summary>
    /// Function to trigger the attack
    /// </summary>
    public void TriggerAttack()
    {
        if (_characterCtrl.weaponEquipped == WeaponEquipped.BOW)
        {
            arrowPrefab.SetActive(false);
            GameObject arrow = Instantiate(shottingArrowPrefab, shootingPoint.position, shootingPoint.rotation);
            arrow.GetComponent<ArrowBehavior>().InitArrow(shootingPoint.forward, _characterCtrl.Character);
            _characterCtrl.SpendStamina(2, null);
        }
        else
        {
            _characterCtrl.SpendStamina(2, () => { _actionTrigger.gameObject.SetActive(true); });
        }
    }

    /// <summary>
    /// Function to stop the action trigger
    /// </summary>
    public void StopTriggerAttack()
    {
        _actionTrigger.gameObject.SetActive(false);
    }

    /// <summary>
    /// Function to active again the arrow
    /// </summary>
    public void ReloadArrow()
    {
        arrowPrefab.SetActive(true);
    }
    #endregion

    #region Weapon functions
    #region Equip and Unequip functions  
    /// <summary>
    /// Function to equip
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="weaponState"></param>
    private void EquipWeapon(WeaponEquipped weapon, float weaponState)
    {
        _characterCtrl.weaponEquipped = weapon;
        _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 1);
        _characterCtrl.anim.SetBool("EquipWeapon", true);
        StartCoroutine(EquipFinishRoutine(weaponState));
    }

    /// <summary>
    /// Function to unequip
    /// </summary>
    /// <param name="characterState"></param>
    /// <param name="weaponID"></param>
    private void Unequipping(float characterState, int weaponID)
    {
        _characterCtrl.isAiming = false;
        _characterCtrl.weaponEquipped = WeaponEquipped.NONE;
        _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 1);
        _characterCtrl.anim.SetBool("EquipWeapon", false);
        _characterCtrl.anim.SetInteger("WeaponID", weaponID);
        StartCoroutine(UnequipFinishRoutine(characterState));
    }

    /// <summary>
    /// Routine to equip the weapon
    /// </summary>
    /// <param name="weaponState"></param>
    /// <returns></returns>
    IEnumerator EquipFinishRoutine(float weaponState)
    {
        yield return new WaitForSeconds(0.5f);

        WeaponStates();

        yield return new WaitForSeconds(0.8f);

        _characterCtrl.anim.SetLayerWeight(_characterCtrl.backActionLayer, 0);

        yield return new WaitForSeconds(0.1f);

        isWeaponEquipped = true;
        DOTween.To(x => SetAnimation(x, weaponState), 0, 1, 0.5f);
        _isEquipping = false;
    }

    /// <summary>
    /// Routine to unequip the weapon
    /// </summary>
    /// <param name="weaponState"></param>
    /// <returns></returns>
    IEnumerator UnequipFinishRoutine(float weaponState)
    {
        yield return new WaitForSeconds(0.35f);

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
    #endregion

    /// <summary>
    /// Function to set the weapon states
    /// </summary>
    private void WeaponStates()
    {
        switch (_characterCtrl.weaponEquipped)
        {
            case WeaponEquipped.SWORD:
                swordPrefab.SetActive(true);
                shieldPrefab.SetActive(true);
                bowPrefab.SetActive(false);
                arrowPrefab.SetActive(false);
                axePrefab.SetActive(false);
                break;
            case WeaponEquipped.BOW:
                swordPrefab.SetActive(false);
                shieldPrefab.SetActive(false);
                bowPrefab.SetActive(true);
                arrowPrefab.SetActive(true);
                axePrefab.SetActive(false);
                break;
            case WeaponEquipped.AXE:
                swordPrefab.SetActive(false);
                shieldPrefab.SetActive(true);
                bowPrefab.SetActive(false);
                arrowPrefab.SetActive(false);
                axePrefab.SetActive(true);
                break;
            case WeaponEquipped.NONE:
                swordPrefab.SetActive(false);
                shieldPrefab.SetActive(false);
                bowPrefab.SetActive(false);
                arrowPrefab.SetActive(false);
                axePrefab.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion
}
