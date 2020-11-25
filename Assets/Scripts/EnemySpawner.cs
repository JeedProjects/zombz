using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    private int targetCount = 2;
    public int currentCount = 0;
    private int spawnedCount = 0;
    public float spawnDelay = 2;
    public int currentWave = 0;

    void Start()
    {
        currentCount = targetCount;
        foreach (Transform child in transform)
        {
            StartCoroutine(Spawner(child));
        }
    }

    void Update()
    {
        if (currentCount <= 0)
        {
            targetCount += targetCount;
            currentCount = targetCount;
            currentWave += 1;
            spawnedCount = 0;
            foreach (Transform child in transform)
            {
                StartCoroutine(Spawner(child));
            }
        }
    }

    IEnumerator Spawner(Transform transform)
    {
        while (spawnedCount < targetCount) 
        {
            spawnedCount += 1;
            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemy.GetComponent<EnemyBehaviour>().target = player;
            enemy.GetComponent<EnemyBehaviour>().spawner = this;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
