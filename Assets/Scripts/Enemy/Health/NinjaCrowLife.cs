using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCrowLife : MonoBehaviour
{
    private Animator animator;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void HandleTakeDamage()
    {
        animator.SetTrigger("GotHit");
        audioManager.PlaySFX(audioManager.crowHit);
    }
}
