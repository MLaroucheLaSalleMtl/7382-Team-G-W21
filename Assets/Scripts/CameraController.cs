using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Main Camera")]
    public Transform mainCamT;

    [Header("Third person camera")]
    public GameObject thirdPersonCamContainer;
    public CinemachineFreeLook thirdPersonCamera;

    [Header("Aiming camera")]
    public GameObject aimingCamContainer;
    public CinemachineVirtualCamera aimingCamera;


    [Header("Control variables")]
    [Range(0, 10)]
    public float sensitivityX = 1f;
    [Range(0, 10)]
    public float sensitivityY = 1f;
    private float _sensitivityX;
    private float _sensitivityY;

    private void Awake()
    {
        instance = this;
    }

    public void SetCameraToNormal()
    {
        aimingCamContainer.SetActive(false);
        thirdPersonCamContainer.SetActive(true);
    }

    public void SetCameraToAiming()
    {
        thirdPersonCamContainer.SetActive(false);
        aimingCamContainer.SetActive(true);
    }

    public void OnMovingMouse(InputAction.CallbackContext context)
    {
        if (Gamemanager.instance.IsPlayerDead()) return;

        Vector2 delta = context.ReadValue<Vector2>();

        _sensitivityX = sensitivityX * 0.01f;
        _sensitivityY = sensitivityY * 0.0001f;

        thirdPersonCamera.m_XAxis.Value += delta.x * _sensitivityX;
        thirdPersonCamera.m_YAxis.Value -= delta.y * _sensitivityY;
    }

    /// <summary>
    /// Function useful for tweening a vector2
    /// </summary>
    /// <param name="axis"></param>
    public void FixAxisVector(Vector2 axis)
    {
        thirdPersonCamera.m_XAxis.Value = axis.x;
        thirdPersonCamera.m_YAxis.Value = axis.y;
    }

    /// <summary>
    /// Function to set the aiming camera
    /// </summary>
    /// <param name="targetAngle"></param>
    public void CallAimCamera(float targetAngle)
    {
        StartCoroutine(RoutineToAim(targetAngle));
    }

    /// <summary>
    /// Routine to control the camera changing
    /// </summary>
    /// <param name="targetAngle"></param>
    /// <returns></returns>
    IEnumerator RoutineToAim(float targetAngle)
    {
        thirdPersonCamera.m_XAxis.m_MinValue = -180f + targetAngle;
        thirdPersonCamera.m_XAxis.m_MaxValue = 180f + targetAngle;

        Vector2 currentAxis = new Vector2(thirdPersonCamera.m_XAxis.Value, thirdPersonCamera.m_YAxis.Value);
        Vector2 destAxis = new Vector2(targetAngle, 0.5f);
        DOTween.To(() => currentAxis, x => { currentAxis = x; FixAxisVector(currentAxis); }, destAxis, 0.5f);

        yield return new WaitForSeconds(0.5f);
        SetCameraToAiming();
    }
}
