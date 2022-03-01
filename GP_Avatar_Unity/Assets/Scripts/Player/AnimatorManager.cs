using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator _animator;
    private int _horizontal;
    private int _vertical;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _horizontal = Animator.StringToHash("Horizontal");
        _vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        _animator.SetFloat(_horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        _animator.SetFloat(_vertical, verticalMovement, 0.1f, Time.deltaTime);
    }
}
