using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    public float damage = 100f;
    private bool isBeingThrown=false;
    private float timer=0.1f;

    private Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isBeingThrown)
        {
            timer-=Time.deltaTime;
            if(timer<=0f && rigidBody2D.velocity.magnitude<=0.5f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Toss()
    {
        isBeingThrown = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBeingThrown)
        {
            if(collision.gameObject.tag=="Enemy")
            {
                Debug.Log("Enemy Attacked");
                EnemyLife enemyLife = collision.gameObject.GetComponent<EnemyLife>();

                enemyLife.TakeDamage(damage);
            }
            if (collision.gameObject.tag == "Tyson")
            {
                TysonHealth tysonHealth = collision.gameObject.GetComponent<TysonHealth>();

                tysonHealth.TakeDamage(damage);
                return;
            }
        }
    }
}
