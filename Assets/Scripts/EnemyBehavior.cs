using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private bool nearTarget = false;
    static float maxDistanceToTarget = 2;

    private float attackActionCooldownTimer;
    private float attackActionCooldown = 2;
    private bool readyToAttack;

    private EnemyRPGStat characterStats;
    private GameObject kiBlast;
    public GameObject KiBlastPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
        characterStats = gameObject.GetComponent<EnemyRPGStat>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (characterStats.IsDead) { return; }

        nearTarget = Vector3.Distance(transform.position, player.transform.position) > maxDistanceToTarget;

        if (nearTarget)
        {
            agent.destination = player.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }

        // attackActionCooldownTimer += Time.deltaTime;
        // if (attackActionCooldownTimer > attackActionCooldown) { readyToAttack = true; attackActionCooldownTimer = 0; }

        if (readyToAttack)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToTarget > 5 && distanceToTarget < 10)
            {
                Debug.Log(Vector3.Distance(transform.position, player.transform.position));
                kiBlast = Instantiate(KiBlastPrefab, transform.position, transform.rotation);
                kiBlast.gameObject.GetComponent<kiBlastProjectile>().Initialize(characterStats);
                kiBlast.gameObject.GetComponent<kiBlastProjectile>().launchProjectile(player.transform.position);
                readyToAttack = false;
            }
        }
        else
        {
            attackActionCooldownTimer += Time.deltaTime;
            if (attackActionCooldownTimer > attackActionCooldown)
            {
                readyToAttack = true;
                attackActionCooldownTimer = 0;
            }
        }
    }
}
