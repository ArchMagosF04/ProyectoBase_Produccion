using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    AudioManager manager;
    public float healingAmount=50f;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Healed");
            collision.gameObject.GetComponent<PlayerHealth>().GainHealth(healingAmount);
            manager.PlaySFX(manager.healing);
            Destroy(gameObject);
        }
    }
}
