using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    grab,
    interact
}

public class PlayerCombo : MonoBehaviour
{
    public PlayerState currentState;
    public Animator animator;
    public int combo = 1;
    public bool isAttacking;
    private string punchName = "Punch";
    private string kickName = "Kick";

    [SerializeField] private PlayerDamage playerDamage; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Combos();
    }

    public void Combos()
    {
        if(Input.GetMouseButtonDown(0) && !isAttacking && currentState!=PlayerState.grab && currentState!=PlayerState.interact) 
        {
            isAttacking = true;
            animator.SetTrigger(punchName+combo);
        }
        if(Input.GetMouseButtonDown(1) && !isAttacking && currentState!=PlayerState.grab && currentState != PlayerState.interact)
        {
            isAttacking = true;
            animator.SetTrigger(kickName+combo);
        }
    }

    public void StartCombo()
    {
        isAttacking=false;
        if(combo<4)
        {
            combo++;
        }
    }

    public void FinishAnimation()
    {
        isAttacking=false;
        combo = 1;
        StartBranch();
    }

    public void StartBranch()
    {
        punchName = "Punch";
        kickName = "Kick";
    }

    public void BranchA()
    {
        punchName = "PunchA";
        kickName = "KickA";
    }

    public void BranchB()
    {
        punchName = "PunchB";
        kickName = "KickB";
    }

    public void BranchC()
    {
        punchName = "PunchC";
        kickName = "KickC";
    }

    public void BranchD()
    {
        punchName = "PunchD";
        kickName = "KickD";
    }

    public void SendDamage()
    {
        float damage = animator.GetFloat("AttackDamage");
        playerDamage.GetComponent<PlayerDamage>().RecieveDamage(damage);
    }

    public void ChangeState(PlayerState state)
    {
        currentState = state;
    }
}
