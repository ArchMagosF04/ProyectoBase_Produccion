using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Idle : StateMachineBehaviour
{
    bool hasGivenInput;
    PickUp pickUp;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasGivenInput = false;
        pickUp=animator.GetComponent<PickUp>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!hasGivenInput)
        {
            if (Input.GetMouseButtonDown(0) && pickUp.IsHoldingItem == false && ScreenManager.Instance.isPaused==false)
            {
                animator.SetTrigger("Punch1");
                hasGivenInput = true;
            }
            if (Input.GetMouseButtonDown(1) && pickUp.IsHoldingItem == false && ScreenManager.Instance.isPaused == false)
            {
                animator.SetTrigger("Kick1");
                hasGivenInput = true;
            }
            if(Input.GetKeyDown(KeyCode.Space) && animator.GetBool("CanDash"))
            {
                animator.SetBool("IsDashing", true);
                hasGivenInput = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Punch1");
        animator.ResetTrigger("Kick1");
        //animator.SetBool("IsIdle", false);
    }
}
