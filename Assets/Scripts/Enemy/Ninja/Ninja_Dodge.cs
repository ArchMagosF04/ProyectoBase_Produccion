using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_Dodge : StateMachineBehaviour
{
    [SerializeField] private float DashSpeed = 20f;

    EnemyLife health;
    NinjaCrowController controller;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponent<NinjaCrowController>();
        health = animator.GetComponent<EnemyLife>();
        health.isInvulnerable = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.Dash(-DashSpeed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        health.isInvulnerable = false;
        controller.ActivateDodgeCooldown();
    }
}
