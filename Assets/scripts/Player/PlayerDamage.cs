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
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Attacked");
            EnemyLife enemyLife = collision.gameObject.GetComponent<EnemyLife>();

            enemyLife.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Tyson")
        {
            TysonHealth tysonHealth = collision.gameObject.GetComponent<TysonHealth>();

            tysonHealth.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Bobby")
        {
            BobbyHealth bobbyHealth = collision.gameObject.GetComponent<BobbyHealth>();

            bobbyHealth.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Bubble")
        {
            HigorShield bubbleShield = collision.gameObject.GetComponent<HigorShield>();

            bubbleShield.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Higor")
        {
            HigorHealth health = collision.gameObject.GetComponent<HigorHealth>();
            health.TakeDamage(damage);
        }
    }
}
