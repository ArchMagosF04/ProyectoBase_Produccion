using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NinjaState
{
    idle,
    moving,
    attack,
    dodge
}

public class NinjaCrowMovement : MonoBehaviour
{
    public NinjaState currentState;

    [SerializeField] private float speed = 6f;

    private Animator animator;

    public Transform target;
    [SerializeField] private float lineOfSight;
    [SerializeField] private float dodgeReactionRange;
    [SerializeField] private float attackRange;
    private Rigidbody2D rigidBody;
    private Vector2 movement;

    private bool attackCooldown;
    private float attackTimer = 3f;
    private bool dodgeCooldown;
    private float dodgeTimer=8f;

    private float distanceFromPlayer;

    private bool hasDetectedPlayer = false;

    [SerializeField] private float DashSpeed = -40f;
    [SerializeField] private float DodgeDuration = 0.5f;
    [SerializeField] private float DiveDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != NinjaState.attack)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rigidBody.rotation = angle;
            direction.Normalize();
            movement = direction;
        }
        

        distanceFromPlayer = Vector2.Distance(target.position, transform.position);

        CheckPlayerDistance();

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Dodge();
        }
        Attack();

        AttackCooldown();
        DodgeCooldown();
    }

    private void Dodge()
    {
        if(distanceFromPlayer <= dodgeReactionRange)
        {
            if(!dodgeCooldown)
            {
                currentState = NinjaState.dodge;
                animator.SetTrigger("Dodge");
                dodgeTimer = 8f;
                dodgeCooldown= true;
            }
        }
    }

    private void Attack()
    {
        if (distanceFromPlayer <= attackRange && currentState == NinjaState.idle)
        {
            if (!attackCooldown)
            {
                currentState = NinjaState.attack;
                animator.SetTrigger("StartAttack");
                attackTimer = 3.5f;
                attackCooldown = true;
            }
        }
    }

    private void CheckPlayerDistance()
    {
        if(distanceFromPlayer > lineOfSight || distanceFromPlayer<=attackRange) 
        {
            if(currentState!=NinjaState.attack)
            {
                currentState = NinjaState.idle;
            }
        }
        if(distanceFromPlayer > attackRange && distanceFromPlayer <=lineOfSight && currentState!=NinjaState.attack && currentState!=NinjaState.dodge)
        {
            currentState = NinjaState.moving;
            if (!hasDetectedPlayer)
            {
                lineOfSight += 50f;
                hasDetectedPlayer = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentState==NinjaState.moving)
        {
            animator.SetBool("isMoving", true);
            MoveEnemy(movement);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void MoveEnemy(Vector2 direction)
    {
        rigidBody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void AttackCooldown()
    {
        if (attackCooldown == true)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackCooldown = false;
            }
        }
    }

    private void DodgeCooldown()
    {
        if (dodgeCooldown == true)
        {
            dodgeTimer -= Time.deltaTime;
            if (dodgeTimer <= 0)
            {
                dodgeCooldown = false;
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

    public IEnumerator SmokeBomb()
    {
        currentState = NinjaState.dodge;
        rigidBody.velocity = new Vector2(movement.x * DashSpeed, movement.y * DashSpeed);
        yield return new WaitForSeconds(DodgeDuration); //espera deteminados segundos, los determinados por la varible que este entro los "()".
        currentState = NinjaState.idle;
    }

    public IEnumerator AttackDive()
    {
        currentState = NinjaState.attack;
        rigidBody.velocity = new Vector2(movement.x * 35f, movement.y * 35f);
        yield return new WaitForSeconds(DiveDuration); //espera deteminados segundos, los determinados por la varible que este entro los "()".
        currentState = NinjaState.idle;
    }

    public void ResetCooldown()
    {
        attackCooldown = true;
        attackTimer = 2.5f;
    }
}
