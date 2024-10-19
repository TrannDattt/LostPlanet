using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class SpawnPosition : ABorder
{
    public EnemyType[] enemyTypes;
    public List<GameObject> enemies { get; private set; }

    public void Init()
    {
        enemies = new List<GameObject>();

        foreach (EnemyType enemyType in enemyTypes)
        {
            //enemyCount += enemy.count;
            for (int i = 0; i < enemyType.count; i++)
            {
                enemies.Add(enemyType.enemy);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.parent.GetComponent<EnemySpawner>().SetActivePosition(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Player"))
        {
            transform.parent.GetComponent<EnemySpawner>().SetActivePosition(null);
        }
    }
}


[System.Serializable]
public class EnemyType
{
    public GameObject enemy;
    public int count;
}
