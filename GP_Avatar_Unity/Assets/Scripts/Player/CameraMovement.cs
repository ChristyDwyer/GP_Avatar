using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private InputManager _inputManager;
    private GameObject _cameraTarget;

    public float camRotationSpeed = 7;
    public float minVerticalAngle = 40;
    public float maxVerticalAngle = 340;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _cameraTarget = GameObject.FindWithTag("Camera Target");
    }

    public void HandleRotation()
    {
        _cameraTarget.transform.rotation *= 
            Quaternion.AngleAxis(_inputManager.camHorizontalInput * camRotationSpeed, Vector3.up) // Horizontal
            * Quaternion.AngleAxis(_inputManager.camVerticalInput * camRotationSpeed, Vector3.right); // Vertical

        Vector3 angles = _cameraTarget.transform.localEulerAngles;
        angles.z = 0;

        angles.x = Mathf.Clamp(angles.x, minVerticalAngle, maxVerticalAngle);

        _cameraTarget.transform.localEulerAngles = angles;
    }
}
