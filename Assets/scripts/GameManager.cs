using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject doors;

    public int enemiesKilled;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        doors.gameObject.SetActive(false);
    }

    public void CloseDoors()
    {
        doors.gameObject.SetActive(true);
    }

    public void OpenDoors()
    {
        doors.gameObject.SetActive(false);
    }
}
