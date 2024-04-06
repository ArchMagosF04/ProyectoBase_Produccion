using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
   // public UnityEvent onDeath = new UnityEvent();
    //private GameObject GB;
    [SerializeField] private int Maxhealth = 100;
    [SerializeField] private int CurrentHealth;

    // Update is called once per frame
    void Start()
    {
       // GB=GetComponent<GameObject>();
        CurrentHealth = Maxhealth;
    }

    public void TakeDamage(int Damage) 
    {
        CurrentHealth -= Damage;
        if (CurrentHealth <= 0) 
        {
            //onDeath.Invoke();
            Die();
        }
    }
    private void Die() 
    {
        Destroy(this.gameObject);
    }
}
