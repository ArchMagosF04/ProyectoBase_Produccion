using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private bool wasActivated;

    private bool wasTriggered;

    public int enemiesToKill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && wasActivated == false)
        {
            GameManager.Instance.CloseDoors();
            wasActivated = true;
        }
    }

    private void Update()
    {
        if(GameManager.Instance.enemiesKilled==enemiesToKill && wasTriggered==false) 
        {
            GameManager.Instance.OpenDoors();
            wasTriggered = true;
        }
    }
}
