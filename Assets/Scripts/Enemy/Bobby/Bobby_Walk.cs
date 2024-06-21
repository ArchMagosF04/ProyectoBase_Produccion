using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobby_Walk : StateMachineBehaviour
{
    public float speed = 4.8f;

    Transform player;
    Rigidbody2D rigidbody;
    BobbyController controller;

    private Vector3 direction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.GetComponent<Rigidbody2D>();
        controller = animator.GetComponent<BobbyController>();

        direction = player.position - animator.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
        direction.Normalize();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody.MovePosition(animator.transform.position + (direction * speed * Time.fixedDeltaTime));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.ActivateChargeCooldown();
        animator.SetBool("IsWalking", false);
    }
}
