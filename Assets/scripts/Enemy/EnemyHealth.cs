using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int Maxhealth = 100;
    [SerializeField] private int CurrentHealth;

    // Update is called once per frame
    void Start()
    {
       // GB=GetComponent<GameObject>();
        CurrentHealth = Maxhealth;
    }

    public void TakeDamage(int damage) 
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) 
        {
            Die();
        }
    }
    private void Die() 
    {
        Destroy(this.gameObject);
    }
}
