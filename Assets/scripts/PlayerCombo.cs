using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    public Animator animator;
    public int combo=1;
    public bool isAttacking;
    private string punchName = "Punch";
    private string kickName = "Kick";

    [SerializeField] public Collider2D hitbox;

    private List<Collider2D> collidersDamaged;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collidersDamaged = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Combos();

        if (animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }
    }

    public void Combos()
    {
        if(Input.GetMouseButtonDown(0) && !isAttacking) 
        {
            isAttacking = true;
            animator.SetTrigger(punchName+combo);
        }
        if(Input.GetMouseButtonDown(1) && !isAttacking)
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

    private void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitbox, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                // Only check colliders with a valid Team Componnent attached
                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    Debug.Log("Enemy Has Taken:" + combo + "Damage");
                    collidersDamaged.Add(collidersToDamage[i]);
                    collidersToDamage[i].GetComponent<EnemyHealth>().TakeDamage(5);
                }
            }
        }

        collidersDamaged.Clear();
    }
}
