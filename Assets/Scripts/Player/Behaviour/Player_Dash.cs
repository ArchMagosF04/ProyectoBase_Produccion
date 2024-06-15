using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : StateMachineBehaviour
{
    [SerializeField] private float DashSpeed = 20f;

    private Vector2 MoveInput;

    PlayerHealth health;
    Movement movement;
    Rigidbody2D playerRB;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerRB=animator.GetComponent<Rigidbody2D>();
        movement=animator.GetComponent<Movement>();
        health=animator.GetComponent<PlayerHealth>(); 
        health.isInvulnerable=true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MoveInput=movement.MoveInput;
        playerRB.velocity = new Vector2(MoveInput.x * DashSpeed, MoveInput.y * DashSpeed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        health.isInvulnerable = false;
        animator.SetBool("IsDashing", false);
        animator.SetBool("CanDash", false);
        animator.SetBool("IsIdle", true);
    }
}
