using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float rotationSpeed;
    public Transform front;
    public GameObject punch;

    private bool launchAttack;
    private bool inRange;
    private bool onCooldown;
    private float Timer;
    private float attackTime=0.2f;

    void Start()
    {
        Timer = 1.3f;
        player = GameObject.FindGameObjectWithTag("Player");
        punch.gameObject.SetActive(false);
    }


    void Update()
    {
        Vector2 direction=(player.transform.position - transform.position).normalized;
        float distance=Vector2.Distance(transform.position, player.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);
        //Debug.DrawRay(transform.position,direction*distance, Color.yellow);

        ChasePlayer(direction);
        //AttackRange();
        AttackCooldown();
        AttackDissipate();
    }

    void ChasePlayer(Vector2 direction)
    {
        float angle=Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle-90);

        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);

    }

    void AttackRange()
    {
        Vector2 direction = front.up;

        RaycastHit2D hit2 = Physics2D.Raycast(front.transform.position, direction, 1);
        Debug.DrawRay(front.transform.position, direction * 1, Color.red);

        if(hit2.collider !=null)
        {
            if (hit2.collider.CompareTag("Player")) 
            {
                AttackPlayer();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player in Range");
            if(onCooldown==false)
            {
                Invoke("AttackPlayer", 0.5f);
                //AttackPlayer();
            }
        }
    }

    void AttackCooldown()
    {
        if (onCooldown == true)
        {
            Timer-= Time.deltaTime;
            if(Timer <= 0)
            {
                onCooldown = false;
            }
        }
    }

    void AttackPlayer()
    {
        if(onCooldown==false)
        {
            Debug.Log("Punch");
            Timer = 1.3f;
            launchAttack = true;
            punch.gameObject.SetActive(true);
            onCooldown=true;
        }
    }

    void AttackDissipate()
    {
        if (launchAttack == true)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                launchAttack = false;
            }
        }
        if (launchAttack == false)
        {
            punch.gameObject.SetActive(false);
            attackTime = 0.2f;
        }
    }
}
