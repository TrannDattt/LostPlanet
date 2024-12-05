using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMPPooling : Singleton<TMPPooling>
{
    private Queue<PopUpText> damageTMPQueue = new();
    private Queue<PopUpText> coinTMPQueue = new();
    private Queue<Slider> enemyHpBarQueue = new();

    public PopUpText damageTMP;
    public PopUpText coinTMP;
    public Slider enemyHpBar;

    public Canvas canvas;

    public void ReturnObjectToPool(PopUpText obj)
    {
        switch (obj.textType)
        {
            case ETextType.Damage:
                damageTMPQueue.Enqueue(obj);
                break;

            case ETextType.Coin:
                coinTMPQueue.Enqueue(obj);
                break;

            default:
                break;
        }
    }

    public void ReturnHpBarToPool(Slider hpBar)
    {
        enemyHpBarQueue.Enqueue(hpBar);
    }

    public void SpawnDamageTMP(Vector2 spawnPos, string text)
    {
        if(damageTMPQueue.Count == 0)
        {
            PopUpText newDamageTMP = Instantiate(damageTMP, canvas.transform);
            damageTMPQueue.Enqueue(newDamageTMP);
        }

        damageTMPQueue.Dequeue().SetInstacne(spawnPos, text);
    }

    public void SpawnCoinTMP(Vector2 spawnPos, string text)
    {
        if (coinTMPQueue.Count == 0)
        {
            PopUpText newCoinTMP = Instantiate(coinTMP, canvas.transform);
            coinTMPQueue.Enqueue(newCoinTMP);
        }

        coinTMPQueue.Dequeue().SetInstacne(spawnPos, text);
    }

    public Slider SpawnHpBar(Vector2 spawnPos)
    {
        if(enemyHpBarQueue.Count == 0)
        {
            var newEnemyHpBar = Instantiate(enemyHpBar, canvas.transform);
            enemyHpBarQueue.Enqueue(newEnemyHpBar);
        }

        var spawnedHpBar = enemyHpBarQueue.Dequeue();
        spawnedHpBar.transform.position = spawnPos;

        return spawnedHpBar;
    }
}
