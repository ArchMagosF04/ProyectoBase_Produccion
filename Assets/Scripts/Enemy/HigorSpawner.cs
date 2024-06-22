using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject basicCrow;
    [SerializeField] private GameObject ninjaCrow;
    [SerializeField] private GameObject strongCrow;

    public Transform[] spawnPoints;

    public void Wave1()
    {
        GameObject bCrow = Instantiate(basicCrow);
        GameObject bCrow2 = Instantiate(basicCrow);
        GameObject bCrow3 = Instantiate(basicCrow);
        bCrow.transform.position = spawnPoints[0].transform.position;
        bCrow2.transform.position = spawnPoints[1].transform.position;
        bCrow3.transform.position = spawnPoints[2].transform.position;
    }

    public void Wave2()
    {
        GameObject bCrow = Instantiate(ninjaCrow);
        GameObject bCrow2 = Instantiate(ninjaCrow);
        GameObject bCrow3 = Instantiate(basicCrow);
        bCrow.transform.position = spawnPoints[0].transform.position;
        bCrow2.transform.position = spawnPoints[1].transform.position;
        bCrow3.transform.position = spawnPoints[2].transform.position;
    }

    public void Wave3()
    {
        GameObject bCrow = Instantiate(strongCrow);
        GameObject bCrow2 = Instantiate(strongCrow);
        GameObject bCrow3 = Instantiate(basicCrow);
        bCrow.transform.position = spawnPoints[0].transform.position;
        bCrow2.transform.position = spawnPoints[1].transform.position;
        bCrow3.transform.position = spawnPoints[2].transform.position;
    }
}
