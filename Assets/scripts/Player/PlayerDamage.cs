using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float damage;

    public void RecieveDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            Debug.Log("Enemy Attacked");
            EnemyLife enemyLife = collision.gameObject.GetComponent<EnemyLife>();

            enemyLife.TakeDamage(damage);
        }
    }
}
