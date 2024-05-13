using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //public int enemyCount;
    //public int enemyKilled;

    //public GameObject Goal;

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

    private void Start()
    {
        //EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();

        //Goal.SetActive(false);

        //foreach (var enemy in enemies)
        //{
        //    enemyCount++;
        //}
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ReturnMenu();
        }
        //if (enemyKilled==enemyCount)
        //{
        //    Goal.SetActive(true);
        //}
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void Death()
    {
        //SceneManager.LoadScene("Level 1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //public void EnemyDestroyed()
    //{
    //    enemyKilled++;
    //}

}
