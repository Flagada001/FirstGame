using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject character;
    private NavMeshAgent agent;
    private bool nearTarget = false;
    static float maxDistanceToTarget = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        nearTarget = Vector3.Distance(transform.position, character.transform.position) > maxDistanceToTarget;

        if (agent.enabled) //Would be disabled if dead
        {
            if (nearTarget)
            {
                agent.destination = character.transform.position;
            }
            else
            {
                agent.destination = transform.position;
            }
        }

        // spawnTimer += Time.deltaTime;
        // if (!hasSpawned && spawnTimer >= 3)
        // {
        //     GameObject instance = Instantiate(gameObject, transform.position, transform.rotation);
        //     hasSpawned = true;
        // }
    }
}
