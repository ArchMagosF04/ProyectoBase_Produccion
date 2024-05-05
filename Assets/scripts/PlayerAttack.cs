using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private float kickSpeed=0.7f;
    [SerializeField] private float punchSpeed=0.2f;

    [SerializeField] private float punchDamage=10f;
    [SerializeField] private float kickDamage=25f;

    private float damage;

    float timeUntilKick;
    float timeUntilPunch;

    private void Update()
    {
        Kick();
        Punch();
    }

    private void Kick()
    {
        if (timeUntilKick <= 0f)
        {
            if (Input.GetMouseButtonDown(1))
            {
                damage = kickDamage;
                anim.SetTrigger("Kick");
                timeUntilKick = kickSpeed;
                //Debug.Log("Kick");
            }
        }
        else
        {
            timeUntilKick -= Time.deltaTime;
        }
    }

    private void Punch()
    {
        if (timeUntilPunch <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                damage = punchDamage;
                anim.SetTrigger("Punch");
                timeUntilPunch = punchSpeed;
                //Debug.Log("Kick");
            }
        }
        else
        {
            timeUntilPunch -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("Enemy hit");
        }
    }
 
}
