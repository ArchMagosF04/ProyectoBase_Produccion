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

    //private Animator animator;

    [SerializeField] private GameObject corpse;

    //AudioManager audioManager;


    private void Awake()
    {
        //audioManager=GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
        //animator = this.GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        //animator.SetTrigger("GotHit");
        //audioManager.PlaySFX(audioManager.crowHit);
        OnTakingDamage?.Invoke();

        health-=damage;

        Debug.Log("Enemy Health: " + health);

        if(health <= 0)
        {
            GameObject enemyCorpese = Instantiate(corpse);
            enemyCorpese.transform.position=transform.position;
            enemyCorpese.transform.rotation=transform.rotation;

            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
