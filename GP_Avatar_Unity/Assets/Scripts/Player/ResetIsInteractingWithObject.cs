using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsInteractingWithObject : StateMachineBehaviour
{
    private int _objectInteracting;
    
    private void Awake()
    {
        _objectInteracting = Animator.StringToHash("isInteractingWithObject");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_objectInteracting, false);
    }
}
