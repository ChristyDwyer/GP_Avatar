using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    private Renderer _myRenderer;
    private Camera _mainCamera;
    
    private void Awake()
    {
        _myRenderer = GetComponent<Renderer>();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (_myRenderer.isVisible)
        {
            transform.LookAt(_mainCamera.transform.position);
        }
    }
}
