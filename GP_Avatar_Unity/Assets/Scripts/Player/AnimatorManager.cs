using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    
    private int _horizontal;
    private int _vertical;
    private int _interacting;
    private int _grounded;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _horizontal = Animator.StringToHash("Horizontal");
        _vertical = Animator.StringToHash("Vertical");
        _interacting = Animator.StringToHash("isInteracting");
        _grounded = Animator.StringToHash("isGrounded");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting, bool isGrounded)
    {
        if (isSprinting)
        {
            verticalMovement = 2;
        }
        
        animator.SetFloat(_horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(_vertical, verticalMovement, 0.1f, Time.deltaTime);
        animator.SetBool(_grounded, isGrounded);
    }
    
    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.CrossFade(targetAnimation, 0.1f);
        animator.SetBool(_interacting, isInteracting);
    }
}
