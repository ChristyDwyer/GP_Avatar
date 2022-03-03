using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager _inputManager;
    
    public Camera cameraObject;
    public float rotationSpeed = 15;
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 7;
    
    private Vector3 _moveDirection;
    private Rigidbody _playerRigidBody;
    private GameObject _cameraTarget;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main;
        _cameraTarget = GameObject.FindGameObjectWithTag("Camera Target");
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
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

        _cameraTarget.transform.position = transform.position + new Vector3(0, 1.6f, 0);
    }

    private void HandleRotation()
    {
        /*
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
        */

        /*
        float rotateAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + cameraObject.transform.eulerAngles.y;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref rotationSpeed, 0.1f);
        transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        Vector3 camForward = Quaternion.Euler(0, rotateAngle, 0).normalized * Vector3.forward;

        camForward *= runningSpeed * Time.deltaTime;
        transform.Translate(camForward, Space.World);
        */

        Quaternion targetRotation = Quaternion.LookRotation(cameraObject.transform.forward);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, playerRotation.y, 0);
    }
}
