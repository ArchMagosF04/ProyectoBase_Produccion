using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TysonController : MonoBehaviour
{
    [SerializeField] private float lineOfSight;
    [SerializeField] private float attackRange;
    [SerializeField] private float spinRange;

    public float LineOfSight=> lineOfSight;
    public float AttackRange => attackRange;
    public float SpinRange => spinRange;

    private Transform player;
    private float distanceFromPlayer;
    private Animator animator;

    public bool meleeCooldown;
    [SerializeField] private float meleeAttackTimer = 3.5f;
    private float meleeAttackTimerRecord;

    public bool spinCooldown;
    [SerializeField] private float spinAttackTimer = 15f;
    private float spinAttackTimerRecord;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        meleeAttackTimerRecord=meleeAttackTimer;
        spinAttackTimerRecord=spinAttackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpinCooldown();
        AttackCooldown();

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
        {
            animator.SetBool("isMoving", true);
        }
    }

    public void StartBoss()
    {
        lineOfSight = 75f;
        spinRange = 9f;
        spinCooldown = true;
        spinAttackTimer = spinAttackTimerRecord;
    }



    public void ActivateAttackCooldown()
    {
        meleeAttackTimer = meleeAttackTimerRecord;
        meleeCooldown = true;
    }

    public void ActivateSpinCooldown()
    {
        spinCooldown = true;
        spinAttackTimer = spinAttackTimerRecord;
    }

    private void AttackCooldown()
    {
        if (meleeCooldown == true)
        {
            meleeAttackTimer -= Time.deltaTime;
            if (meleeAttackTimer <= 0)
            {
                meleeCooldown = false;
                meleeAttackTimer = meleeAttackTimerRecord;
            }
        }
    }

    private void SpinCooldown()
    {
        if (spinCooldown == true)
        {
            spinAttackTimer -= Time.deltaTime;
            if (spinAttackTimer <= 0)
            {
                spinCooldown = false;
                spinAttackTimer = spinAttackTimerRecord;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spinRange);
    }
}
