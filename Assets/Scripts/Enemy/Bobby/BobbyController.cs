using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbyController : MonoBehaviour
{
    [SerializeField] private float lineOfSight;

    public float LineOfSight => lineOfSight;

    private float distanceFromPlayer;
    private Animator animator;

    public bool chargeCooldown;
    [SerializeField] private float chargeAttackTimer = 3.5f;
    private float chargeAttackTimerRecord;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        chargeAttackTimerRecord=chargeAttackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        ChargeAttackCooldown();
    }

    public void StartBoss()
    {
        lineOfSight = 50f;
        chargeCooldown = true;
        chargeAttackTimer = 1.5f;
    }

    public void ActivateChargeCooldown()
    {
        chargeAttackTimer = chargeAttackTimerRecord;
        chargeCooldown = true;
    }

    private void ChargeAttackCooldown()
    {
        if (chargeCooldown == true)
        {
            chargeAttackTimer -= Time.deltaTime;
            if (chargeAttackTimer <= 0)
            {
                chargeCooldown = false;
                chargeAttackTimer = chargeAttackTimerRecord;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
