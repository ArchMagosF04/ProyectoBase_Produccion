using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float lineOfSight;
    [SerializeField] private float attackRange;

    public float LineOfSight => lineOfSight;
    public float AttackRange => attackRange;

    private Transform player;
    private float distanceFromPlayer;
    private Animator animator;

    public bool meleeCooldown;
    [SerializeField] private float meleeAttackTimer = 1.5f;
    private float meleeAttackTimerRecord;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        meleeAttackTimerRecord = meleeAttackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        AttackCooldown();

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
    }

    public void ActivateAttackCooldown()
    {
        meleeAttackTimer = meleeAttackTimerRecord;
        meleeCooldown = true;
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
