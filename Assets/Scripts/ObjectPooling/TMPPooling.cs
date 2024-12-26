using System.Collections.Generic;
using UnityEngine;

public class TMPPooling : Singleton<TMPPooling>
{
    private Queue<PopUpText> enemyDamageTMPQueue = new();
    private Queue<PopUpText> playerDamageTMPQueue = new();
    private Queue<PopUpText> coinTMPQueue = new();

    public PopUpText enemyDamageTMP;
    public PopUpText playerDamageTMP;
    public PopUpText coinTMP;

    public Canvas canvas;

    public void ReturnObjectToPool(PopUpText obj)
    {
        switch (obj.textType)
        {
            case ETextType.PlayerDamage:
                playerDamageTMPQueue.Enqueue(obj);
                break;

            case ETextType.EnemyDamage:
                enemyDamageTMPQueue.Enqueue(obj);
                break;

            case ETextType.Coin:
                coinTMPQueue.Enqueue(obj);
                break;

            default:
                break;
        }
    }

    private PopUpText GetPUT(PopUpText put, ref Queue<PopUpText> putQueue, string content, Vector2 spawnPos)
    {
        PopUpText spawnPUT = putQueue.Count == 0 ? Instantiate(playerDamageTMP, canvas.transform) : putQueue.Dequeue();
        spawnPUT.Init(content, spawnPos);
        return spawnPUT;
    }

    public PopUpText GetFromPool(PopUpText put, string content, Vector2 spawnPos)
    {
        return put.textType switch
        {
            ETextType.PlayerDamage => GetPUT(playerDamageTMP, ref playerDamageTMPQueue, content, spawnPos),
            ETextType.EnemyDamage => GetPUT(enemyDamageTMP, ref enemyDamageTMPQueue, content, spawnPos),
            ETextType.Coin => GetPUT(coinTMP, ref coinTMPQueue, content, spawnPos),
            _ => null,
        };
    }

    private void ResetPool()
    {
        playerDamageTMPQueue = new();
        enemyDamageTMPQueue = new();
        coinTMPQueue = new();
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
