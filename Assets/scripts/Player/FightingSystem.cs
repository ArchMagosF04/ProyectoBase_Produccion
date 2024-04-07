using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightingSystem : MonoBehaviour
{
    [SerializeField] private Transform KickPoint;
    [SerializeField] private GameObject KickSprite;
    [SerializeField] private Transform PunchPoint;
    [SerializeField] private GameObject PunchSprite;
    [SerializeField] private float KickRange = 0.5f;
    [SerializeField] private float PunchRange = 0.5f;
    [SerializeField] private int Damage = 20;
    [SerializeField] private LayerMask EnemyLayers;
    [Header("DoNOTouch")]
    [SerializeField] private float AttackRate = 0.25f;
    [SerializeField] private float NextAttackTime;

    private void Start()
    {
        KickSprite.SetActive(false);
        PunchSprite.SetActive(false);
    }

    void Update()
    {
        if( NextAttackTime > 0) 
        {
            NextAttackTime -= Time.deltaTime;
        }
        else
        {
            KickSprite.SetActive(false);
            PunchSprite.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1") && NextAttackTime <= 0f)
        {
            PunchSprite.SetActive(true);
            AttackRate = 0.25f;
            Attack(PunchPoint, PunchRange);
            NextAttackTime = AttackRate;
        }
        if (Input.GetButtonDown("Fire2") && NextAttackTime <= 0f)
        {
            KickSprite.SetActive(true);
            AttackRate = 1.10f;
            Attack(KickPoint, KickRange);
            NextAttackTime = AttackRate;
        }
    }

    void Attack(Transform AttackpPoint, float AttackRange ) 
    {
        Collider2D[] hitEnemie = Physics2D.OverlapCircleAll(AttackpPoint.position, AttackRange, EnemyLayers);//dibuja las Hit box de ataques.

        foreach (Collider2D enemy in hitEnemie) //envia daño al enemigo
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(Damage);
        }
    }

    private void OnDrawGizmosSelected()//funcion de hacer visibles las hit box de los ataques.
    {
        if (KickPoint == null || PunchPoint == null) return;
        Gizmos.DrawWireSphere(KickPoint.position, KickRange);
        Gizmos.DrawWireSphere(PunchPoint.position, PunchRange);
    }
}
