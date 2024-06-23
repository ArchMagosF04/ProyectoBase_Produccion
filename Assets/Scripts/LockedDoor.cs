using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private int numberOfLocks = 4;

    private int locksUncloked;

    private void Start()
    {
        locksUncloked = 0;
    }

    public void KeyObtained()
    {
        locksUncloked++;
        if(locksUncloked >= numberOfLocks)
        {
            Destroy(gameObject);
        }
    }
}
