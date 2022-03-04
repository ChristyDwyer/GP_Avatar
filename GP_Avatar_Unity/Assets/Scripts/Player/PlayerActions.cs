using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerManager _playerManager;
    private AnimatorManager _animatorManager;

    public bool isAttacking;
    private int _attackingAnimBool;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _animatorManager = GetComponent<AnimatorManager>();
        
        _attackingAnimBool = Animator.StringToHash("isAttacking");
    }

    public void HandleAttack()
    {
        if(!_playerManager.isInteracting && !_animatorManager.animator.GetBool(_attackingAnimBool))
        {
            _animatorManager.animator.SetBool(_attackingAnimBool, true);
            _animatorManager.PlayTargetAnimation("Attack", true);
        }
    }
}
