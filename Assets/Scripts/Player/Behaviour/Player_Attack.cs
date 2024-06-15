using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Player_Attack : StateMachineBehaviour
{
    AudioManager audioManager;

    public string punchAttack;
    public string kickAttack;

    public int attackType;

    public float damage;

    bool hasGivenAttackInput;

    [SerializeField] private Movement movement;

    public bool isFinisher;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        movement= animator.GetComponent<Movement>();
        hasGivenAttackInput = false;
        movement.SendDamage(damage);
        if(attackType==1)
        {
            audioManager.PlaySFX(audioManager.punch);
        }
        else if(attackType==2)
        {
            audioManager.PlaySFX(audioManager.kick);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!hasGivenAttackInput && !isFinisher)
        {
            if(Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger(punchAttack);
                hasGivenAttackInput= true;
                animator.SetBool("IsIdle",false);
            }
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger(kickAttack);
                hasGivenAttackInput= true;
                animator.SetBool("IsIdle",false);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (!hasGivenAttackInput)
        //{
        //    //animator.SetBool("IsIdle", true);
        //    animator.SetTrigger("GoIdle");
        //}
        animator.ResetTrigger(punchAttack);
        animator.ResetTrigger(kickAttack);
    }
}
