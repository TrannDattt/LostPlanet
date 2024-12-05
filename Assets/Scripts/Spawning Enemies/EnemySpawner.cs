using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    // Spawn enemies
    public List<EnemySpawnedType> enemySpawnedTypes;

    public BoxCollider2D spawnRange;

    public float startSpawnTime = 1;
    public float spawnRate = 0.5f;

    //private void Start()
    //{
        //EnemyPooling.Instance.SpawnFromPool(enemySpawnedTypes[0].enemy, GetRandomSpawnPos());
    //}

    void Update()
    {
        if (enemySpawnedTypes.Count > 0)
        {
            Invoke(nameof(SpawnEnemy), spawnRate);
        }
        //StartCoroutine(SpawnEnemy());
    }

    private void SpawnEnemy()
    {
        var enemySpawnedType = new EnemySpawnedType();
        if (enemySpawnedTypes.Count > 0)
        {
            enemySpawnedType = enemySpawnedTypes[0];
        }
        
        if (enemySpawnedType.amount > 0)
        {
            EnemyPooling.Instance.SpawnFromPool(enemySpawnedType.enemy, GetRandomSpawnPos());
            enemySpawnedType.amount--;
        }

        if (enemySpawnedType.amount == 0 && enemySpawnedTypes.Count > 0)
        {
            enemySpawnedTypes.RemoveAt(0);
        }
    }

    private Vector2 GetRandomSpawnPos()
    {
        float topBound = spawnRange.bounds.max.y;
        float bottomBound = spawnRange.bounds.min.y;
        float leftBound = spawnRange.bounds.min.x;
        float rightBound = spawnRange.bounds.max.x;

        return new Vector2(Random.Range(leftBound, rightBound), Random.Range(bottomBound, topBound));
    }
}

[System.Serializable]
public class EnemySpawnedType
{
    public Enemy enemy;
    public int amount;
}
