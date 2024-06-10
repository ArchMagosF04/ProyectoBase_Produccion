using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UniversalTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger?.Invoke();
        Destroy(gameObject);
    }
}
