using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : Singleton<EnemyPooling>
{
    private Queue<Enemy> babyBoxerQueue = new();
    private Queue<Enemy> bncBotQueue = new();
    private Queue<Enemy> toasterBotQueue = new();
    private Queue<Enemy> mudGuardQueue = new();
    private Queue<Enemy> stormHeadQueue = new();
    private Queue<Enemy> zapBotQueue = new();
    private Queue<Enemy> guardianQueue = new();
    private Queue<Enemy> botWheelQueue = new();
    private Queue<Enemy> smallMonsQueue = new();

    [SerializeField] private Enemy babyBoxer;
    [SerializeField] private Enemy toasterBot;
    [SerializeField] private Enemy bncBot;
    [SerializeField] private Enemy mudGuard;
    [SerializeField] private Enemy guardian;
    [SerializeField] private Enemy stormHead;
    [SerializeField] private Enemy zapBot;
    [SerializeField] private Enemy smallMons;
    [SerializeField] private Enemy botWheel;

    public void ReturnObjectToPool(Enemy enemy)
    {
        switch (enemy.EnemyType)
        {
            case EEnemyType.BabyBoxer:
                babyBoxerQueue.Enqueue(enemy);
                break;

            case EEnemyType.ToasterBot :
                toasterBotQueue.Enqueue(enemy);
                break;

            case EEnemyType.BncBot :
                bncBotQueue.Enqueue(enemy);
                break;

            case EEnemyType.MudGuard :
                mudGuardQueue.Enqueue(enemy);
                break;

            case EEnemyType.StormHead :
                stormHeadQueue.Enqueue(enemy);
                break;

            case EEnemyType.ZapBot :
                zapBotQueue.Enqueue(enemy);
                break;

            case EEnemyType.Guardian :
                guardianQueue.Enqueue(enemy);
                break;

            case EEnemyType.BotWheel :
                botWheelQueue.Enqueue(enemy);
                break;

            case EEnemyType.SmallMons :
                smallMonsQueue.Enqueue(enemy);
                break;
        }
    }

    public Enemy GetEnemy(Enemy enemyPreb, ref Queue<Enemy> enemyQueue, Vector2 spawnPos)
    {
        Enemy spawnedEnemy;

        if(enemyQueue.Count == 0)
        {
            spawnedEnemy = Instantiate(enemyPreb, spawnPos, Quaternion.identity); 
        }
        else
        {
            spawnedEnemy = enemyQueue.Dequeue(); 
            //Debug.Log("dq");
        }

        spawnedEnemy.transform.position = spawnPos;
        spawnedEnemy.Init();
        return spawnedEnemy;
    }

    public Enemy GetFromPool(Enemy enemy, Vector2 spawnPos)
    {
        return enemy.EnemyType switch
        {
            EEnemyType.BabyBoxer => GetEnemy(babyBoxer, ref babyBoxerQueue, spawnPos),
            EEnemyType.ToasterBot => GetEnemy(toasterBot, ref toasterBotQueue, spawnPos),
            EEnemyType.BncBot => GetEnemy(bncBot, ref bncBotQueue, spawnPos),
            EEnemyType.MudGuard => GetEnemy(mudGuard, ref mudGuardQueue, spawnPos),
            EEnemyType.StormHead => GetEnemy(stormHead, ref stormHeadQueue, spawnPos),
            EEnemyType.ZapBot => GetEnemy(zapBot, ref zapBotQueue, spawnPos),
            EEnemyType.Guardian => GetEnemy(guardian, ref guardianQueue, spawnPos),
            EEnemyType.BotWheel => GetEnemy(botWheel, ref botWheelQueue, spawnPos),
            EEnemyType.SmallMons => GetEnemy(smallMons, ref smallMonsQueue, spawnPos),
            _ => null,
        };
    }

    private void ResetPool()
    {
        babyBoxerQueue = new();
        bncBotQueue = new();
        toasterBotQueue = new();
        mudGuardQueue = new();
        stormHeadQueue = new();
        zapBotQueue = new();
        guardianQueue = new();
        botWheelQueue = new();
        smallMonsQueue = new();
    }

    private void Start()
    {
        GameManager.Instance.OnLevelStarted += ResetPool;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelStarted -= ResetPool;
    }
}
