using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject character;
    private NavMeshAgent agent;
    private bool nearTarget = false;
    static float maxDistanceToTarget = 2;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");

    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (gameObject.GetComponent<EnemyRPGStat>().IsDead) { return; }

        nearTarget = Vector3.Distance(transform.position, character.transform.position) > maxDistanceToTarget;

        if (nearTarget)
        {
            agent.destination = character.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }
}
