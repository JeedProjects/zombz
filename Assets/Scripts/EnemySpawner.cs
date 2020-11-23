using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform target;
    public int count;
    public float delay;
    
    void Start()
    {
        StartCoroutine("Spawner");
    }

    IEnumerator Spawner()
    {
        int currentCount = 0;
        while (currentCount < count) 
        {
            GameObject enemy = Instantiate(prefab, transform.position, transform.rotation);
            enemy.GetComponent<EnemyBehaviour>().target = target;
            currentCount += 1;
            yield return new WaitForSeconds(delay);
        }
    }
}
