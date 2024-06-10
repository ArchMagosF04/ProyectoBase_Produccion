using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{

    public float healingAmount=50f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Healed");
            collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(healingAmount);
            Destroy(gameObject);
        }
    }
}
