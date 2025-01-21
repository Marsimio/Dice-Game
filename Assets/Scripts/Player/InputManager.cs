using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class InputManager : MonoBehaviour
{
    private PlayerControls _playerControls;
    private PlayerControls.OnFootActions _onFootActions;

    private PlayerMotors _motor;
    private PlayerLook _playerLook;
    private PlayerAttack _playerAttack;
    void Awake()
    {
        _playerControls = new PlayerControls();
        _onFootActions = _playerControls.OnFoot;
        _motor = GetComponent<PlayerMotors>();
        _playerLook = GetComponent<PlayerLook>();
        _playerAttack = GetComponent<PlayerAttack>();
        
        AssignInputs();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void AssignInputs()
    {
        _onFootActions.Jump.performed += ctx => _motor.Jump();
        _onFootActions.Attack.started += ctx => _playerAttack.Attack();
    }
    void FixedUpdate()
    {
        _motor.ProcessMove(_onFootActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _playerLook.ProcessLook(_onFootActions.Look.ReadValue<Vector2>());
    }

    private void Update()
    {
        if (_onFootActions.Attack.IsPressed())
        {
            _playerAttack.Attack();
        }
    }

    private void OnEnable()
    {
        _onFootActions.Enable();
    }

    private void OnDisable()
    {
        _onFootActions.Disable();
    }
}
