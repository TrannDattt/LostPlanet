using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [System.Serializable]
    public class EnemySpawnedType
    {
        public Enemy enemy;
        public int amount;
    }

    [System.Serializable]
    public class EnemyWave
    {
        public List<EnemySpawnedType> enemySpawnedTypes;
    }

    // Spawn enemies
    [SerializeField] private List<EnemyWave> enemyWaves;
    [SerializeField] private BoxCollider2D spawnRange;
    private float startSpawnTime = 3;

    private int enemyCount;
    private bool isSpawning;

    //private void Start()
    //{
    //    SpawnEnemyWave(0);
    //    curWaveIndex = 0;
    //}

    void Update()
    {
        if(enemyCount == 0)
        {
            if(enemyWaves.Count == 0)
            {
                GameManager.Instance.LevelCleared();
                enemyCount--;
            }
            else if(!isSpawning)
            {
                isSpawning = true;
                StartSpawning();
            }
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyWave());
    }

    private IEnumerator SpawnEnemyWave()
    {
        yield return new WaitForSeconds(startSpawnTime);

        foreach (var enemyType in enemyWaves[0].enemySpawnedTypes)
        {
            for (int counter = 0; counter < enemyType.amount; counter++)
            {
                var spawnedEnemy = EnemyPooling.Instance.GetFromPool(enemyType.enemy, GetRandomSpawnPos());
                spawnedEnemy.Dying += SpawnedEnemyDied;
                enemyCount++;
            }
        }

        enemyWaves.RemoveAt(0);
        isSpawning = false;
    }

    private Vector2 GetRandomSpawnPos()
    {
        float topBound = spawnRange.bounds.max.y;
        float bottomBound = spawnRange.bounds.min.y;
        float leftBound = spawnRange.bounds.min.x;
        float rightBound = spawnRange.bounds.max.x;

        return new Vector2(Random.Range(leftBound, rightBound), Random.Range(bottomBound, topBound));
    }

    private void SpawnedEnemyDied()
    {
        enemyCount--;
    }

    private void OnEnable()
    {
        //GameManager.Instance.OnLevelStarted += StartSpawning;
    }

    private void OnDisable()
    {
        //GameManager.Instance.OnLevelStarted -= StartSpawning;
    }
}
