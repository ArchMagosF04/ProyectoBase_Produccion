using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public AIPath aIPath;

    Vector2 direction;

    private Animator animator;
    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        FaceDirection();

        if (rigidBody.velocity.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void FaceDirection()
    {
        direction = aIPath.desiredVelocity;

        transform.right = direction;
    }
}
