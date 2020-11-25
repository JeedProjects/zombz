using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject target;
    public int health;
    public int damage;
    public EnemySpawner spawner;

    private NavMeshAgent agent;
    private Vector3 destination;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    void Update()
    {
        if (Vector3.Distance(destination, target.transform.position) > 1.0f)
        {
            destination = target.transform.position;
            agent.destination = destination;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            spawner.currentCount -= 1;
        }
    }
}
