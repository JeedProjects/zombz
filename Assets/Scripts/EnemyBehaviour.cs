using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform target;
    public int health;
    public int damage;

    private NavMeshAgent agent;
    private Vector3 destination;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    void Update()
    {
        if (Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }
    }
}
