using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAiming : MonoBehaviour
{
    private CharacterCtrl _characterCtrl;

    private bool _isTargeting;

    // Start is called before the first frame update
    void Awake()
    {
        _characterCtrl = GetComponent<CharacterCtrl>();
    }

    private void Update()
    {
        if (_characterCtrl.isAiming)
        {
            if (!_isTargeting)
            {
                _isTargeting = true;
                CameraController.instance.CallAimCamera(_characterCtrl.transform.eulerAngles.y);                
            }

            //_characterCtrl.transform.rotation = Quaternion.Lerp(_characterCtrl.transform.rotation, Quaternion.Euler(0, CameraController.instance.mainCamT.eulerAngles.y, 0), 0.5f);            
            //CameraController.instance.thirdPersonCamera.m_XAxis.Value = 0;
        }
        else
        {
            CameraController.instance.SetCameraToNormal();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (_characterCtrl.weaponEquipped == WeaponEquipped.BOW)
        {
            if (context.performed)
            {
                _characterCtrl.isAiming = true;
            }
            else if (context.canceled)
            {
                _isTargeting = false;
                _characterCtrl.isAiming = false;
            }
        }
    }
}
