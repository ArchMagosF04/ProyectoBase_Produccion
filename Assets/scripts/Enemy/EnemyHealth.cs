using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float Maxhealth = 100f;
    [SerializeField] private float CurrentHealth;

    // Update is called once per frame
    void Start()
    {
       // GB=GetComponent<GameObject>();
        CurrentHealth = Maxhealth;
    }

    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0f) 
        {
            Die();
        }
    }
    private void Die() 
    {
        Destroy(this.gameObject);
    }
}
