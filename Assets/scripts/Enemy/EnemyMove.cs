using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    moving,
    attack
}

public class EnemyMove : MonoBehaviour
{
    public EnemyState currentState;

    [SerializeField] private float speed = 0.5f;

    private Animator animator; 

    public Transform target;
    [SerializeField] private float lineOfSight;
    [SerializeField] private float attackRange;
    private Rigidbody2D rigidBody;
    private Vector2 movement;

    private bool onCooldown;
    private float attackTimer=1f;

    private float distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction= target.position - transform.position;
        float angle=Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        rigidBody.rotation = angle;
        direction.Normalize();
        movement = direction;

        distanceFromPlayer = Vector2.Distance(target.position, transform.position);


        if (distanceFromPlayer <= attackRange)
        {
            if (!onCooldown)
            {
                animator.SetTrigger("StartAttack");
                attackTimer = 1f;
                onCooldown = true;
            }
        }

        AttackCooldown();
    }

    private void FixedUpdate()
    {
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
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
        if (onCooldown == true)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                onCooldown = false;
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

    public void ResetCooldown()
    {
        onCooldown = true;
        attackTimer = 1f;
    }

}
