using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsAttacking : StateMachineBehaviour
{
    private int _attacking;
    
    private void Awake()
    {
        _attacking = Animator.StringToHash("isAttacking");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_attacking, false);
    }
}
