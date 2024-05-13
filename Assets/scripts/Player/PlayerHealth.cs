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

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    public void UpdateHealth(float change)
    {
        health += change;

        Debug.Log("Player Health: " + health);

        if (health>maxHealth)
        {
            health=maxHealth;
        }else if(health<=0f)
        {
            health=0f;
            healthSlider.value = health;

            Destroy(gameObject);
            GameManager.Instance.Death();
        }
    }

    private void OnGUI()
    {
        float smooth=Time.deltaTime/1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value,health,smooth);
    }
}
