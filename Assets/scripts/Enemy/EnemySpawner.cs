using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    private float globalTimer;
    private float spawnTimer;
    private float modSpawnTimer;

    private void Start()
    {
        SpawnEnemies();
        globalTimer = 0f; 
        spawnTimer=10f;
        modSpawnTimer = spawnTimer;
    }


    // Update is called once per frame
    void Update()
    {
        spawnTimer-=Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemies();
            spawnTimer = modSpawnTimer;
        }



        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnEnemies();
        }

        globalTimer += Time.deltaTime;

        if (globalTimer >= 15f && modSpawnTimer>=6f)
        {
            modSpawnTimer -= 1f;
            globalTimer = 0f;
        }
    }

    void SpawnEnemies()
    {
        GameObject enemy = Instantiate(prefab);
        enemy.transform.position = transform.position;
    }
}
