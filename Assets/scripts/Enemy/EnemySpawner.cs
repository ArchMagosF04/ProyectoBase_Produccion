using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] crows;

    public Transform[] spawnPoints;

    public bool isActive=false;

    private bool spawnCooldown=true;
    [SerializeField] private float spawnTime;
    private float initialTime;


    private void Start()
    {
        initialTime = spawnTime;
    }


    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            SpawnOnCooldown();

            if (!spawnCooldown)
            {
                SpawnEnemies();
                spawnCooldown = true;
            }
        }
    }

    public void ActivateSpawner()
    {
        isActive = true;
        spawnCooldown = true;
        spawnTime = initialTime;
    }

    private void SpawnOnCooldown()
    {
        if (spawnCooldown == true)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                spawnCooldown = false;
                spawnTime = initialTime;
            }
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(crows[i]);
            enemy.transform.position = spawnPoints[i].transform.position;
        }
    }
}
