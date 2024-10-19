using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    // Spawn enemies
    public SpawnPosition[] spawnPostions;
    int enemyInThisLevel { get; set; }
    public SpawnPosition activePosition { get; set; }

    GameObject spawnedEnemy;
    GameObject lastEnemy;
    public GameObject boss;

    bool bossSummoned = false;

    float spawnTime = 2;
    float spawnInterval = 5;

    // Start is called before the first frame update
    void Start()
    {
        enemyInThisLevel = 0;
        foreach (SpawnPosition spawnPosition in spawnPostions)
        {
            spawnPosition.Init();
            enemyInThisLevel += spawnPosition.enemies.Count;
        }

        InvokeRepeating("SpawnEnemy", spawnTime, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastEnemy && !bossSummoned)
        {
            SummonBoss();
        }

        if (bossSummoned && boss.GetComponent<EnemyController>().curHealth <= 0)
        {
            FinishStage();
        }
    }

    void SpawnEnemy()
    {
        if (activePosition)
        {
            if (activePosition.enemies.Count > 0)
            {
                int randomIndex = Random.Range(0, activePosition.enemies.Count);
                spawnedEnemy = activePosition.enemies[randomIndex];
                activePosition.enemies.Remove(spawnedEnemy);
                enemyInThisLevel--;

                if (enemyInThisLevel == 0)
                {
                    lastEnemy = spawnedEnemy;
                }

                Instantiate(spawnedEnemy, Helpers.RandomizePosition(activePosition), Quaternion.identity);
            }
        }
    }

    public void SetActivePosition(SpawnPosition position)
    {
        activePosition = position;
    }

    void SummonBoss()
    {
        Instantiate(boss, Helpers.RandomizePosition(activePosition), Quaternion.identity);

        bossSummoned = true;
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
