using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    private bool wasActivated;

    private bool wasTriggered;

    [SerializeField] private GameObject doors;

    [SerializeField] private int enemiesToKill;

    private int enemiesKilled;

    public UnityEvent OnTrigger = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && wasActivated == false)
        {
            doors.SetActive(true);
            OnTrigger?.Invoke();
            wasActivated = true;
        }
    }

    public void OnEnemyKill()
    {
        enemiesKilled++;
    }

    private void Update()
    {
        if(enemiesKilled>=enemiesToKill && wasTriggered==false) 
        {
            doors.SetActive(false);
            wasTriggered = true;
            gameObject.SetActive(false);
        }
    }
}
