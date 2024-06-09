using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TysonHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float health;

    [SerializeField] private Slider healthSlider;

    public bool isInvulnerable;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        healthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        health -= damage;

        Debug.Log("Enemy Health: " + health);

        if (health <= 0)
        {
            health = 0;
            healthSlider.value = health;
            Die(); 
        }
    }

    private void Die()
    {
        animator.SetBool("isDeafeted", true);
    }

    private void OnGUI()
    {
        float smooth = Time.deltaTime / 1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value, health, smooth);
    }
}
