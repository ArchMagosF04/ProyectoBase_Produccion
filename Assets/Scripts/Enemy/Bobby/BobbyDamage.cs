using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbyDamage : MonoBehaviour
{
    public float damage = 35f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Attacked");
            collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-damage);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            EnemyLife enemyLife = collision.gameObject.GetComponent<EnemyLife>();

            enemyLife.TakeDamage(damage);
        }
    }
}
