using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTickDamage : MonoBehaviour
{
    public float damage = 5f;
    [SerializeField] private float damageTickSpeed = 0.4f;
    private float damageTick;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(damageTickSpeed<=damageTick)
            {
                Debug.Log("Player Attacked");
                collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-damage);
                damageTick = 0f;
            } else
            {
                damageTick += Time.deltaTime;
            }
            
        }
    }
}
