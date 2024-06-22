using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Higor_Idle : StateMachineBehaviour
{
    HigorController controller;

    bool actionCooldown=true;
    public float actionTimer=2f;
    float actionTimerRecord;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponent<HigorController>();
        actionTimerRecord = actionTimer;
        actionCooldown = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.FacePlayer();

        if (actionCooldown == true)
        {
            actionTimer -= Time.deltaTime;
            if (actionTimer <= 0)
            {
                actionCooldown = false;
                actionTimer = actionTimerRecord;
            }
        }

        if (!actionCooldown)
        {
            if (animator.GetBool("HasUsedAction")==false)
            {
                if (controller.shouldSummon)
                {
                    animator.SetTrigger("Summon");
                    return;
                }
                if (controller.DistanceFromPlayer <= controller.FlameRange && controller.flameCooldown==false)
                {
                    animator.SetTrigger("Flame");
                    return;
                }
                if(controller.DistanceFromPlayer <= controller.LineOfSight)
                {
                    animator.SetTrigger("Missile");
                    return;
                }

            } else
            {
                animator.SetTrigger("Teleport");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Summon");
        animator.ResetTrigger("Flame");
        animator.ResetTrigger("Missile");
        animator.ResetTrigger("Teleport");
    }
}
