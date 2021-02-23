// Movement solution from Brackeys
// https://www.youtube.com/watch?v=4HpC--2iowE

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _inputAxis = Vector2.zero;

    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;

    private bool _isSprinting;
    private float _normalSpeed, _sprintSpeed;

    private CharacterCtrl _characterCtrl;

    private void Awake()
    {
        _normalSpeed = speed;
        _sprintSpeed = 2 * speed;

        _characterCtrl = GetComponent<CharacterCtrl>();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    /// <summary>
    /// Function to move the character
    /// </summary>
    private void MoveCharacter()
    {
        Vector3 direction = new Vector3(_inputAxis.x, 0f, _inputAxis.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (_isSprinting)
            {
                _characterCtrl.SpendStamina(speed * Time.deltaTime, null, OnEnergyWasted);
            }

            transform.position += moveDir.normalized * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Input OnMove event
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputAxis = context.ReadValue<Vector2>();
    }


    /// <summary>
    /// Input OnSprint event
    /// </summary>
    /// <param name="context"></param>
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isSprinting = true;
            speed = _sprintSpeed;
        }
        else if (context.canceled)
        {
            _isSprinting = false;
            speed = _normalSpeed;
        }
    }

    /// <summary>
    /// Function triggered when there's no energy to spend
    /// </summary>
    private void OnEnergyWasted()
    {
        _isSprinting = false;
        speed = _normalSpeed;
    }
}
