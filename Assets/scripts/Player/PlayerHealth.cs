using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5;
    private float health;
    [SerializeField] private Slider healthSlider;

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyPunch")
        {
            health -= 1;
        }
    }

    private void OnGUI()
    {
            healthSlider.value = health;
    }
}
