using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongCrowLife : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void HandleTakeDamage()
    {
        audioManager.PlaySFX(audioManager.crowHit);
    }
}
