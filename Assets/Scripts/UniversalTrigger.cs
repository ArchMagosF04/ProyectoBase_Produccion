using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UniversalTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            OnTrigger?.Invoke();
            Destroy(gameObject);
        }
    }
}
