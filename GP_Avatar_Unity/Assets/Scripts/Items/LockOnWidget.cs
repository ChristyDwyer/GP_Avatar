using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnWidget : MonoBehaviour
{
    private Renderer _myRenderer;
    private Camera _mainCamera;
    
    private void Awake()
    {
        _myRenderer = GetComponent<SpriteRenderer>();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (_myRenderer.isVisible)
        {
            transform.LookAt(_mainCamera.transform);
        }
    }
}
