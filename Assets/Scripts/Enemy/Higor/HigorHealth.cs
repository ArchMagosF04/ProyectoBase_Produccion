using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HigorHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 1;
    private float health;

    [SerializeField] private Slider healthSlider;

    public bool isInvulnerable;

    private Animator animator;

    public UnityEvent OnDeath = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        healthSlider.maxValue = maxHealth;
        isInvulnerable = true;
    }

    public void Phase2()
    {
        isInvulnerable = false;
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
        animator.SetTrigger("IsDefeated");
    }

    private void OnGUI()
    {
        float smooth = Time.deltaTime / 1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value, health, smooth);
    }
}
