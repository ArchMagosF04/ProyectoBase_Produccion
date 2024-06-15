using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCrowController : MonoBehaviour
{
    [SerializeField] private float lineOfSight;
    [SerializeField] private float attackRange;
    [SerializeField] private float dodgeReactionRange;

    public float LineOfSight => lineOfSight;
    public float AttackRange => attackRange;
    public float DodgeReactionRange => dodgeReactionRange;

    private Transform player;
    private float distanceFromPlayer;
    private Animator animator;

    public bool attackCooldown;
    [SerializeField] private float attackTimer = 3.5f;
    private float attackTimerRecord;

    public bool dodgeCooldown;
    [SerializeField] private float dodgeTimer = 15f;
    private float dodgeTimerRecord;

    private Rigidbody2D rigidBody;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        attackTimerRecord = attackTimer;
        dodgeTimerRecord = dodgeTimer;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.position - animator.transform.position;
        direction.Normalize();

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        DodgeCooldown();
        AttackCooldown();
    }

    public void ActivateAttackCooldown()
    {
        attackTimer = attackTimerRecord;
        attackCooldown = true;
    }

    public void ActivateDodgeCooldown()
    {
        dodgeCooldown = true;
        dodgeTimer = dodgeTimerRecord;
    }

    private void AttackCooldown()
    {
        if (attackCooldown == true)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackCooldown = false;
                attackTimer = attackTimerRecord;
            }
        }
    }

    public void Dash(float DashSpeed)
    {
        rigidBody.velocity = new Vector2(direction.x * DashSpeed, direction.y * DashSpeed);
    }

    public void Dive()
    {
        animator.SetBool("IsDiving", true);
    }

    private void DodgeCooldown()
    {
        if (dodgeCooldown == true)
        {
            dodgeTimer -= Time.deltaTime;
            if (dodgeTimer <= 0)
            {
                dodgeCooldown = false;
                dodgeTimer = dodgeTimerRecord;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dodgeReactionRange);
    }
}
