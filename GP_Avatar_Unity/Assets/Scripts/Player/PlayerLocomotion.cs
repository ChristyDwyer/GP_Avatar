using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private PlayerManager _playerManager;
    private InputManager _inputManager;
    private AnimatorManager _animatorManager;
    private int _jumpingAnimBool;
    
    public Camera cameraObject;

    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;
    
    [Header("Movement")]
    public float rotationSpeed = 10;
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 7;

    [Header("Jumping")]
    public float gravityIntenstity = -15;
    public float jumpHeight = 3;
    public float jumpSpeedModifier = 0.3f;
    
    [Header("Falling")]
    public float inAirTimer;
    public float jumpOffVelocity = 3;
    public float fallSpeed = 33;
    public float raycastHeightOffset = 0.5f;
    public LayerMask groundLayer;
    
    private Vector3 _moveDirection;
    private Rigidbody _playerRigidBody;
    private GameObject _cameraTarget;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _inputManager = GetComponent<InputManager>();
        _animatorManager = GetComponent<AnimatorManager>();
        _playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main;
        _cameraTarget = GameObject.FindGameObjectWithTag("Camera Target");
        
        _jumpingAnimBool = Animator.StringToHash("isJumping");
    }

    public void HandleAllMovement()
    {
        HandlePhysics();
        
        if (!_playerManager.isInteracting)
        {
            HandleMovement();
            
        }
        
        HandleRotation();
        _cameraTarget.transform.position = transform.position + new Vector3(0, 1.6f, 0);
    }

    private void HandleMovement()
    {
        if (isJumping)
            return;

        Transform camTransform = cameraObject.transform;
        _moveDirection = camTransform.forward * _inputManager.verticalInput
                + camTransform.right * _inputManager.horizontalInput;
        _moveDirection.y = 0;
        _moveDirection.Normalize();

        if (isSprinting)
        {
            _moveDirection *= sprintingSpeed;
        }

        else
        {
            if (_inputManager.moveAmount >= 0.5f)
            {
                _moveDirection *= runningSpeed;
            }
        
            else
            {
                _moveDirection *= walkingSpeed;
            }
        }

        if (isGrounded && !isJumping)
        {
            Vector3 movementVelocity = _moveDirection;
            _playerRigidBody.velocity = movementVelocity;
        }
    }

    private void HandleRotation()
    {
        if(isJumping)
            return;
        
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.transform.forward * _inputManager.verticalInput;
        targetDirection += cameraObject.transform.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

                
        float currentRotationSpeed = rotationSpeed;
        
        if (!isGrounded)
        {
            currentRotationSpeed *= jumpSpeedModifier;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, currentRotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandlePhysics()
    {
        Vector3 transformPosition = transform.position;
        
        RaycastHit hit;
        Vector3 raycastOrigin = transformPosition;
        Vector3 targetPosition = transformPosition;
        raycastOrigin.y += raycastHeightOffset;

        if (!isGrounded && !isJumping)
        {
            if (!_playerManager.isInteracting)
            {
                _animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer += Time.deltaTime;
            _playerRigidBody.AddForce(transform.forward * jumpOffVelocity);
            _playerRigidBody.AddForce(-Vector3.up * fallSpeed * inAirTimer);
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, 0.5f, groundLayer))
        {
            if (!isGrounded && _playerManager.isInteracting)
            {
                _animatorManager.PlayTargetAnimation("Land", true);
            }

            Vector3 raycastHitPoint = hit.point;
            targetPosition.y = raycastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }

        if (isGrounded && !isJumping)
        {
            if (_playerManager.isInteracting || _inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    public void HandleJump()
    {
        if (isGrounded && !_playerManager.isInteracting)
        {
            _animatorManager.animator.SetBool(_jumpingAnimBool, true);
            _animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntenstity * jumpHeight);
            Vector3 playerVelocity = _moveDirection * 0.7f;
            playerVelocity.y = jumpingVelocity;
            _playerRigidBody.velocity = playerVelocity;
        }
    }
}
