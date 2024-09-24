using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : Border
{
    public EnemyType[] enemyTypes;
    List<GameObject> enemies = new();
    public List<GameObject> Enemies
    {
        get { return enemies; }
    }

    public void Init()
    {
        foreach (EnemyType enemy in enemyTypes)
        {
            //enemyCount += enemy.count;
            for (int i = 0; i < enemy.count; i++)
            {
                enemies.Add(enemy.enemy);
            }
        }
    }

    public Vector2 RandomizePosition()
    {
        return (new Vector2(Random.Range(LeftBorder, RightBorder), Random.Range(BottomBorder, TopBorder)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name);
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
