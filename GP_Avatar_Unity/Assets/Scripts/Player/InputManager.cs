using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _playerControls;
    private PlayerLocomotion _playerLocomotion;
    private AnimatorManager _animatorManager;
    private PlayerActions _playerActions;

    public Vector2 movementInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public Vector2 cameraInput;
    public float camVerticalInput;
    public float camHorizontalInput;
    
    public bool sprintInput;
    public bool jumpInput;
    public bool attackInput;

    private void Awake()
    {
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _animatorManager = GetComponent<AnimatorManager>();
        _playerActions = GetComponent<PlayerActions>();
    }

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerInputActions();

            _playerControls.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
            _playerControls.Player.Camera.performed += context => cameraInput = context.ReadValue<Vector2>();

            _playerControls.Player.Sprint.performed += context => sprintInput = true;
            _playerControls.Player.Sprint.canceled += context => sprintInput = false;
            
            _playerControls.Player.Jump.performed += context => jumpInput = true;

            _playerControls.Player.Attack.performed += context => attackInput = true;
        }

        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleCameraInput();
        HandleJumpInput();
        HandleAttackInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, moveAmount,
            _playerLocomotion.isSprinting, _playerLocomotion.isGrounded);
    }

    private void HandleCameraInput()
    {
        camVerticalInput = cameraInput.y;
        camHorizontalInput = cameraInput.x;
    }

    private void HandleSprintingInput()
    {
        _playerLocomotion.isSprinting = sprintInput && moveAmount > 0.5f;
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            _playerLocomotion.HandleJump();
        }
    }

    private void HandleAttackInput()
    {
        if (attackInput)
        {
            attackInput = false;
            _playerActions.HandleAttack();
        }
    }
}
