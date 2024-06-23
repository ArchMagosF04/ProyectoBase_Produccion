using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float health;
    [SerializeField] private Slider healthSlider;

    AudioManager audioManager;

    private Animator animator;

    public bool isInvulnerable;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    public void GainHealth(float change)
    {
        health += change;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void UpdateHealth(float change)
    {
        if (isInvulnerable)
        {
            return;
        }
        animator.SetTrigger("GotHurt");
        health += change*PlayerPrefs.GetFloat("EnemyDamageMultiplier");
        audioManager.PlaySFX(audioManager.playerHit);

        Debug.Log("Player Health: " + health);

        if (health>maxHealth)
        {
            health=maxHealth;
        }else if(health<=0f)
        {
            health=0f;
            healthSlider.value = health;

            ScreenManager.Instance.GameOverScreen();
            gameObject.SetActive(false);
        }
    }

    private void OnGUI()
    {
        float smooth=Time.deltaTime/1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value,health,smooth);
    }
}
