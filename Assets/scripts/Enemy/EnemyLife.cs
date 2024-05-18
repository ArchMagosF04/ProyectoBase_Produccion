using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float maxHealth=100;
    private float health;

    private Animator animator;

    [SerializeField] private GameObject corpse;

    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
        animator = this.GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("GotHit");
        health-=damage;

        Debug.Log("Enemy Health: " + health);

        if(health <= 0)
        {
            GameObject enemyCorpese = Instantiate(corpse);
            enemyCorpese.transform.position=transform.position;
            Destroy(gameObject);
        }
    }
}
