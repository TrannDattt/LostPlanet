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
            case EEnemyType.BnCBot:
                bncBotQueue.Enqueue(enemy);
                break;

            case EEnemyType.ToasterBot:
                toasterBotQueue.Enqueue(enemy);
                break;

            case EEnemyType.BabyBoxer:
                babyBoxerQueue.Enqueue(enemy);
                break;

            case EEnemyType.MudGuard:
                mudGuardQueue.Enqueue(enemy);
                break;

            case EEnemyType.StormHead:
                stormHeadQueue.Enqueue(enemy);
                break;

            case EEnemyType.ZapBot:
                zapBotQueue.Enqueue(enemy);
                break;

            case EEnemyType.Guardian:
                guardianQueue.Enqueue(enemy);
                break;

            case EEnemyType.BotWheel:
                botWheelQueue.Enqueue(enemy);
                break;

            case EEnemyType.SmallMons:
                smallMonsQueue.Enqueue(enemy);
                break;
        }
    }

    public void SpawnFromPool(Enemy enemy, Vector2 spawnPos)
    {
        switch (enemy.EnemyType)
        {
            case EEnemyType.BnCBot:
                if(bncBotQueue.Count == 0)
                {
                    var newEnemy = Instantiate(bncBot, spawnPos, Quaternion.identity);
                    bncBotQueue.Enqueue(newEnemy);
                }

                bncBotQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;

            case EEnemyType.ToasterBot:
                if (toasterBotQueue.Count == 0)
                {
                    var newEnemy = Instantiate(toasterBot, spawnPos, Quaternion.identity);
                    toasterBotQueue.Enqueue(newEnemy);
                }

                toasterBotQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;

            case EEnemyType.BabyBoxer:
                if (babyBoxerQueue.Count == 0)
                {
                    var newEnemy = Instantiate(babyBoxer, spawnPos, Quaternion.identity);
                    babyBoxerQueue.Enqueue(newEnemy);
                }

                babyBoxerQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;

            case EEnemyType.MudGuard:
                if (mudGuardQueue.Count == 0)
                {
                    var newEnemy = Instantiate(mudGuard, spawnPos, Quaternion.identity);
                    mudGuardQueue.Enqueue(newEnemy);
                }

                mudGuardQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;

            case EEnemyType.StormHead:
                if (stormHeadQueue.Count == 0)
                {
                    var newEnemy = Instantiate(stormHead, spawnPos, Quaternion.identity);
                    stormHeadQueue.Enqueue(newEnemy);
                }

                stormHeadQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;

            case EEnemyType.ZapBot:
                if (zapBotQueue.Count == 0)
                {
                    var newEnemy = Instantiate(zapBot, spawnPos, Quaternion.identity);
                    zapBotQueue.Enqueue(newEnemy);
                }

                zapBotQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;

            case EEnemyType.Guardian:
                if (guardianQueue.Count == 0)
                {
                    var newEnemy = Instantiate(guardian, spawnPos, Quaternion.identity);
                    guardianQueue.Enqueue(newEnemy);
                }

                guardianQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;

            case EEnemyType.BotWheel:
                if (botWheelQueue.Count == 0)
                {
                    var newEnemy = Instantiate(botWheel, spawnPos, Quaternion.identity);
                    botWheelQueue.Enqueue(newEnemy);
                }

                botWheelQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;

            case EEnemyType.SmallMons:
                if (smallMonsQueue.Count == 0)
                {
                    var newEnemy = Instantiate(smallMons, spawnPos, Quaternion.identity);
                    smallMonsQueue.Enqueue(newEnemy);
                }

                smallMonsQueue.Dequeue().GetComponent<EnemyManager>().Init(spawnPos);
                break;
        }
    }
}
