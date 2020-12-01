using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject target;
    public GameObject zombiePrefab;
    public int targetZombieCount = 2;
    [HideInInspector] public int currentZombieCount;
    public float spawnDelay = 1;
    public float gracePeriod = 2;
    public GameObject currentWaveText;

    private int spawnedZombieCount;
    private int currentWave = 0;

    void Update()
    {
        if (currentZombieCount <= 0)
        {
            currentWave += 1;
            currentWaveText.GetComponent<TextMeshProUGUI>().SetText("Wave " + currentWave.ToString());
            currentZombieCount = targetZombieCount;
            spawnedZombieCount = 0;
            StartCoroutine("Spawner");
        }
        if (spawnedZombieCount >= targetZombieCount)
        {
            targetZombieCount += targetZombieCount;
            StopCoroutine("Spawner");
        }
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(gracePeriod);
        while (true)
        {
            foreach (Transform child in transform)
            {
                spawnedZombieCount += 1;
                GameObject newZombie = Instantiate(zombiePrefab, child.position, child.rotation);
                newZombie.GetComponent<EnemyBehaviour>().target = target;
                newZombie.GetComponent<EnemyBehaviour>().spawner = this;
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
