using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BobbyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float health;

    [SerializeField] private float maxShield = 3;
    private float shield;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider shieldSlider;

    public bool isInvulnerable;

    private Animator animator;

    public UnityEvent OnDeath = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shield = maxShield;
        animator = GetComponent<Animator>();
        healthSlider.maxValue = maxHealth;
        shieldSlider.maxValue = maxShield;
        isInvulnerable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeShieldDamage()
    {
        shield--;
        if(shield <= 0)
        {
            animator.SetTrigger("Stun");
            isInvulnerable = false;
            shield = 0;
            shieldSlider.value = shield;
        }
    }

    public void RegainShield()
    {
        shield = maxShield;
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
            isInvulnerable = true;
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        animator.SetTrigger("isDeafeted");
    }

    private void OnGUI()
    {
        float smooth = Time.deltaTime / 1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value, health, smooth);
        shieldSlider.value = Mathf.Lerp(shieldSlider.value,shield, smooth);
    }
}
