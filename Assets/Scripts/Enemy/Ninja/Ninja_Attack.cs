using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_Attack : StateMachineBehaviour
{
    [SerializeField] private float DashSpeed = 20f;

    NinjaCrowController controller;
    Rigidbody2D rb;
    Transform player;

    Vector3 direction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = animator.GetComponent<NinjaCrowController>();
        rb=animator.GetComponent<Rigidbody2D>();

        direction = player.position - animator.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("IsDiving"))
        {
            rb.velocity = new Vector2(direction.x * DashSpeed, direction.y * DashSpeed);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsDiving", false);
        controller.ActivateAttackCooldown();
    }
}
