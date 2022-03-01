using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager _inputManager;
    
    public GameObject cameraObject;
    public float rotationSpeed = 15;
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 7;
    
    
    private Vector3 _moveDirection;
    private Rigidbody _playerRigidBody;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = GameObject.FindWithTag("Camera Target");
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        //HandleRotation();
    }

    private void HandleMovement()
    {
        _moveDirection = cameraObject.transform.forward * _inputManager.verticalInput;
        _moveDirection += cameraObject.transform.right * _inputManager.horizontalInput;
        _moveDirection.y = 0;
        _moveDirection.Normalize();
        
        if (_inputManager.moveAmount >= 0.5f)
        {
            _moveDirection *= runningSpeed;
        }
        
        else
        {
            _moveDirection *= walkingSpeed;
        }

        Vector3 movementVelocity = _moveDirection;
        _playerRigidBody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.transform.forward * _inputManager.verticalInput;
        targetDirection += cameraObject.transform.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
