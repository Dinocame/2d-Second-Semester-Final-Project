using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnCooldown = 5f;
    public int maxEnemies = 5;

    private float nextSpawnTime = 0f;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Update()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        if (Time.time >= nextSpawnTime && spawnedEnemies.Count < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnCooldown;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(
            enemyPrefab,
            transform.position,
            Quaternion.identity
        );

        spawnedEnemies.Add(enemy);
    }
}