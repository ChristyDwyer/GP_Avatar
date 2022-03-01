using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerLocomotion _playerLocomotion;
    private CameraMovement _cameraMovement;
    
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _cameraMovement = GetComponent<CameraMovement>();
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
}
