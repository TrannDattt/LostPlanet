using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    // Spawn enemies
    public SpawnPosition[] spawnPostions;
    int enemyCount = 0;
    SpawnPosition activePosition;

    GameObject spawnedEnemy;
    GameObject lastEnemy;
    public GameObject boss;

    float spawnTime = 2;
    float spawnInterval = 5;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 0;
        foreach (SpawnPosition spawnPosition in spawnPostions)
        {
            spawnPosition.Init();
            enemyCount += spawnPosition.Enemies.Count;
        }

        InvokeRepeating("SpawnEnemy", spawnTime, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastEnemy && lastEnemy.GetComponent<EnemyController>().Health <= 0)
        {
            SummonBoss();
        }

        if (boss && boss.GetComponent<EnemyController>().Health <= 0)
        {
            FinishStage();
        }
    }

    void SpawnEnemy()
    {
        if (activePosition)
        {
            StartCoroutine(Spawning(activePosition));
        }
    }

    IEnumerator Spawning(SpawnPosition position)
    {
        if(position && position.Enemies != null && position.Enemies.Count > 0)
        {
            int randomIndex = Random.Range(0, position.Enemies.Count);
            spawnedEnemy = position.Enemies[randomIndex];
            position.Enemies.Remove(spawnedEnemy);
            enemyCount--;

            if (enemyCount == 0)
            {
                lastEnemy = spawnedEnemy;
            }

            Instantiate(spawnedEnemy, position.RandomizePosition(), Quaternion.identity);
        }

        yield return null;
    }

    public void SetActivePosition(SpawnPosition position)
    {
        activePosition = position;
    }

    void SummonBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }

    void FinishStage()
    {
        int curLevel = SceneManager.GetActiveScene().buildIndex;

        if (curLevel + 1 > SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            return;
        }
        SceneManager.LoadScene(curLevel + 1);

        PlayerPrefs.SetInt("LevelSave", curLevel + 1);
    }
}


[System.Serializable]
public class EnemyType
{
    public GameObject enemy;
    public int count;
}
