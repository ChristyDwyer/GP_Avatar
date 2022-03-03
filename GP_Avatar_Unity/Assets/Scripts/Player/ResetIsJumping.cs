using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsJumping : StateMachineBehaviour
{
    private int _jumping;
    
    private void Awake()
    {
        _jumping = Animator.StringToHash("isJumping");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_jumping, false);
    }
}
