using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerManager _playerManager;
    private AnimatorManager _animatorManager;

    public bool isAttacking;
    public bool isInteractingWithObject;
    private int _attackingAnimBool;
    private int _objectInteractionAnimBool;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _animatorManager = GetComponent<AnimatorManager>();
        
        _attackingAnimBool = Animator.StringToHash("isAttacking");
        _objectInteractionAnimBool = Animator.StringToHash("isInteractingWithObject");
    }

    public void HandleAttack()
    {
        if(!_playerManager.isInteracting && !_animatorManager.animator.GetBool(_attackingAnimBool))
        {
            _animatorManager.animator.SetBool(_attackingAnimBool, true);
            _animatorManager.PlayTargetAnimation("Attack", true);
        }
    }
    
    public void HandleObjectInteract()
    {
        if(!_playerManager.isInteracting && !_animatorManager.animator.GetBool(_objectInteractionAnimBool))
        {
            _animatorManager.animator.SetBool(_objectInteractionAnimBool, true);
            _animatorManager.PlayTargetAnimation("Object Interaction", true);

            _playerManager.nearestInteractableInRange.GetComponent<InteractionManager>().Interact();
        }
    }
}
