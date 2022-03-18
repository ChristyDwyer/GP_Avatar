using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private PlayerManager _playerManager;
    private InputManager _inputManager;
    private GameObject _cameraTarget;

    public float camRotationSpeed = 7;
    public float minVerticalAngle = 20;
    public float maxVerticalAngle = 320;

    public float snapLerpDuration = 3;
    public float snappedCamHeight = 40;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _inputManager = GetComponent<InputManager>();
        _cameraTarget = GameObject.FindWithTag("Camera Target");
    }

    public void HandleRotation()
    {
        if (_playerManager.isLockedOn)
        {
            _cameraTarget.transform.rotation = gameObject.transform.rotation;
            return;
        }    
            
        if (_inputManager.camSnapInput.magnitude != 0)
        {
            int snapAngle = 0;

            if (_inputManager.camSnapInput.y != 0)
            {
                snapAngle = _inputManager.camSnapInput.y == 1 ? 45 : 225;
            }

            else
            {
                snapAngle = _inputManager.camSnapInput.x == 1 ? 135 : 315;
            }

            Quaternion desiredSnappedAngle = 
                Quaternion.AngleAxis(snapAngle, Vector3.up) // Horizontal
                * Quaternion.AngleAxis(snappedCamHeight, Vector3.right); // Vertical
            
            StopAllCoroutines();
            StartCoroutine(StartSnapCamLerp(desiredSnappedAngle));
        }

        else
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

    private IEnumerator StartSnapCamLerp(Quaternion desiredSnapAngle)
    {
        float elapsedTime = 0;

        while (elapsedTime < snapLerpDuration)
        {
            _cameraTarget.transform.rotation = 
                Quaternion.Slerp(_cameraTarget.transform.rotation, desiredSnapAngle, (elapsedTime / snapLerpDuration));
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        _cameraTarget.transform.rotation = desiredSnapAngle;
        yield return null;
    }
}
