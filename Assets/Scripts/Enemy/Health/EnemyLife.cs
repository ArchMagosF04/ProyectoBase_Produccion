using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyLife : MonoBehaviour
{
    public UnityEvent OnTakingDamage=new UnityEvent();
    public UnityEvent OnDeath=new UnityEvent();

    [SerializeField] private float maxHealth=100;
    private float health;

    public bool isInvulnerable;

    [SerializeField] private GameObject corpse;


    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        OnTakingDamage?.Invoke();

        health-=damage;

        Debug.Log("Enemy Health: " + health);

        if(health <= 0)
        {
            isInvulnerable = true;
            health = 0;
            GameObject enemyCorpese = Instantiate(corpse);
            enemyCorpese.transform.position=transform.position;
            enemyCorpese.transform.rotation=transform.rotation;

            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
