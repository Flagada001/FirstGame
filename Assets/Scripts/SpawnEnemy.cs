using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject SpawnPoint;
    private GameObject enemyObject;
    private Vector3 spawnPoint;
    private float countSecondToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = SpawnPoint.transform.position;
        enemyObject = Instantiate(EnemyPrefab, spawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        countSecondToSpawn += Time.deltaTime;

        // Limit enemy to 5
        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= 5) { return; }

        if (countSecondToSpawn > 3)
        {
            countSecondToSpawn = 0;
            enemyObject = Instantiate(EnemyPrefab, spawnPoint, Quaternion.identity);

        }
    }
}
