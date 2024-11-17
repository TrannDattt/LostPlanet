using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    // Spawn enemies
    public List<EnemyType> enemyTypes;

    public BoxCollider2D spawnRange;

    public float startSpawnTime = 1;
    public float spawnRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startSpawnTime, spawnRate);
        //StartCoroutine(SpawnEnemy());
    }

    private void SpawnEnemy()
    {
        if (enemyTypes.Count <= 0) 
        {
            return;
        }

        EnemyType enemyType = enemyTypes[0];

        if (enemyType.count > 0)
        {
            if (enemyType.enemy.GetType() == typeof(BabyBoxer))
            {
                EnemyPooling.Instance.SpawnBabyBoxer(GetRandomSpawnPos());
            }

            if (enemyType.enemy.GetType() == typeof(ToasterBot))
            {
                EnemyPooling.Instance.SpawnToasterBot(GetRandomSpawnPos());
            }

            if (enemyType.enemy.GetType() == typeof(BncBot))
            {
                EnemyPooling.Instance.SpawnBncBot(GetRandomSpawnPos());
            }

            enemyType.count--;
        }

        if (enemyType.count <= 0)
        {
            enemyTypes.RemoveAt(0);
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
public class EnemyType
{
    public AEnemyController enemy;
    public int count;
}
