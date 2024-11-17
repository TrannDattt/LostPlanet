using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPPooling : Singleton<TMPPooling>
{
    private Queue<PopUpText> damageTMPQueue = new Queue<PopUpText>();
    private Queue<PopUpText> coinTMPQueue = new Queue<PopUpText>();
    //private Queue<PopUpText> damageTMPQueue = new Queue<PopUpText>();

    public PopUpText damageTMP;
    public PopUpText coinTMP;

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
}
