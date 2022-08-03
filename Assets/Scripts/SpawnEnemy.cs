using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private GameObject enemyObject;
    private Vector3 spawnPoint = new Vector3(54, 4, 40);
    private float countSecondToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        enemyObject = Instantiate(EnemyPrefab, spawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        countSecondToSpawn += Time.deltaTime;
        if (countSecondToSpawn > 3)
        {
            countSecondToSpawn = 0;
            enemyObject = Instantiate(EnemyPrefab, spawnPoint, Quaternion.identity);
        }
    }
}
