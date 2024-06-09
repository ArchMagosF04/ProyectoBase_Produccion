using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyson_SpinAttack : StateMachineBehaviour
{
    TysonController controller;

    public float speed = 4.8f;

    Transform player;
    Rigidbody2D rigidbody;
    private float distanceFromPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.GetComponent<Rigidbody2D>();
        controller = animator.GetComponent<TysonController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = player.position - animator.transform.position;
        direction.Normalize();

        distanceFromPlayer = Vector2.Distance(player.position, animator.transform.position);

        rigidbody.MovePosition(animator.transform.position + (direction * speed * Time.fixedDeltaTime));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.spinCooldown = true;
        Debug.Log("SetSpinCooldown");
    }
}
