using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyson_Walk : StateMachineBehaviour
{

    public float speed = 4.8f;

    Transform player;
    Rigidbody2D rigidbody;
    TysonController controller;

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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
        direction.Normalize();

        distanceFromPlayer = Vector2.Distance(player.position, animator.transform.position);

        if (distanceFromPlayer > controller.LineOfSight)
        {
            animator.SetBool("isMoving", false);
        }
        if (distanceFromPlayer < controller.LineOfSight && distanceFromPlayer > controller.AttackRange)
        {
            rigidbody.MovePosition(animator.transform.position + (direction * speed * Time.fixedDeltaTime));
        }
        if (distanceFromPlayer <= controller.SpinRange && controller.spinCooldown == false)
        {
            animator.SetTrigger("SpinAttack");
        }
        if (distanceFromPlayer <= controller.AttackRange && controller.meleeCooldown == false)
        {
            animator.SetTrigger("MeleeAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isMoving", false);
        animator.ResetTrigger("MeleeAttack");
        animator.ResetTrigger("SpinAttack");
    }
}
