using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerManager _playerManager;
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
    public bool contextualInput;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
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

            _playerControls.Player.ContextAction.performed += context => contextualInput = true;
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
        HandleContextInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = _playerManager.inCutscene ? 0f : Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
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

    private void HandleContextInput()
    {
        if (contextualInput)
        {
            contextualInput = false;

            if (_playerManager.inInteractRange)
            {
                _playerActions.HandleObjectInteract();
            }

            else
            {
                _playerActions.HandleAttack();
            }
        }
    }

    public void HandleCutsceneStart()
    {
        moveAmount = 0;
        _playerControls.Disable();
        _playerManager.inCutscene = true;
    }

    public void HandleCutsceneEnd()
    {
        _playerControls.Enable();
        _playerManager.inCutscene = false;
    }
}
