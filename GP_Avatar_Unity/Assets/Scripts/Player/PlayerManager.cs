using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerLocomotion _playerLocomotion;
    private PlayerActions _playerActions;
    private CameraMovement _cameraMovement;
    private Animator _animator;

    public bool inCutscene;
    public bool inInteractRange;
    public GameObject nearestInteractableInRange;
    public bool isInteracting;
    private int _interacting;
    private int _jumping;
    private int _attacking;
    private int _interactingWithObject;

    public float speedPowerupModifier;
    
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _playerActions = GetComponent<PlayerActions>();
        _cameraMovement = GetComponent<CameraMovement>();
        _animator = GetComponent<Animator>();
        
        _interacting = Animator.StringToHash("isInteracting");
        _jumping = Animator.StringToHash("isJumping");
        _attacking = Animator.StringToHash("isAttacking");
        _interactingWithObject = Animator.StringToHash("isInteractingWithObject");
    }

    private void Update()
    {
        _inputManager.HandleAllInputs();
        _cameraMovement.HandleRotation();
    }
    
    private void FixedUpdate()
    {
        _playerLocomotion.HandleAllMovement();
    }
    private void LateUpdate()
    {
        isInteracting = _animator.GetBool(_interacting);
        _playerLocomotion.isJumping = _animator.GetBool(_jumping);
        _playerActions.isAttacking = _animator.GetBool(_attacking);
        _playerActions.isInteractingWithObject = _animator.GetBool(_interactingWithObject);
    }
    
    public void HandleSpeedBoost(bool boost, float speedMod)
    {
        _playerLocomotion.hasSpeedBoost = boost;
        speedPowerupModifier = speedMod;
    }

    public void HandleDoubleJumpEffect(bool effect)
    {
        _playerLocomotion.hasDoubleJumpEffect = effect;
    }

    public void ToggleInInteractRange(GameObject interactable)
    {
        inInteractRange = !inInteractRange;

        nearestInteractableInRange = nearestInteractableInRange == null ? interactable : null;
    }
}
