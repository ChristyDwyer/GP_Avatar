using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _playerControls;
    private AnimatorManager _animatorManager;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    public Vector2 cameraInput;
    public float camVerticalInput;
    public float camHorizontalInput;

    public float moveAmount;

    private void Awake()
    {
        _animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerInputActions();

            _playerControls.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
            _playerControls.Player.Camera.performed += context => cameraInput = context.ReadValue<Vector2>();
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
        HandleCameraInput();
    }
    
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, moveAmount);
    }
    
    private void HandleCameraInput()
    {
        camVerticalInput = cameraInput.y;
        camHorizontalInput = cameraInput.x;
    }
}
