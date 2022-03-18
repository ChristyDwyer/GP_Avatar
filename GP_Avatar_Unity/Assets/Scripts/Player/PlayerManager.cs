using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    
    public GameObject lockOnCamera;
    public float lockOnRange = 10f;
    public bool isLockedOn;
    public GameObject lockOnTarget;
    public GameObject lockOnWidget;

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
        
        lockOnWidget = GameObject.FindWithTag("Targetting Widget");
        
        _interacting = Animator.StringToHash("isInteracting");
        _jumping = Animator.StringToHash("isJumping");
        _attacking = Animator.StringToHash("isAttacking");
        _interactingWithObject = Animator.StringToHash("isInteractingWithObject");
    }

    private void Update()
    {
        lockOnCamera.SetActive(isLockedOn);
        _inputManager.HandleAllInputs();
        _cameraMovement.HandleRotation();
    }
    
    private void FixedUpdate()
    {
        _playerLocomotion.HandleAllMovement();

        if (lockOnTarget == null)
        {
            lockOnCamera.GetComponent<CinemachineVirtualCamera>().LookAt = null;
            lockOnWidget.GetComponent<SpriteRenderer>().enabled = false;
        }

        else
        {
            lockOnWidget.GetComponent<SpriteRenderer>().enabled = true;
        }
        
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.forward),
               out hit, lockOnRange));
        {
            if (hit.collider != null  && hit.collider.gameObject.CompareTag("Targetable"))
            {
                lockOnTarget = hit.collider.gameObject;
                lockOnCamera.GetComponent<CinemachineVirtualCamera>().LookAt = lockOnTarget.transform;
                lockOnWidget.transform.position = lockOnTarget.transform.position + new Vector3(0, 2, 0);
            }
        }
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
