using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : Singleton<EnemyPooling>
{
    private Queue<BabyBoxer> babyBoxerQueue = new Queue<BabyBoxer>();
    private Queue<ToasterBot> toasterBotQueue = new Queue<ToasterBot>();
    private Queue<BncBot> bncBotQueue = new Queue<BncBot>();

    public BabyBoxer babyBoxer;
    public ToasterBot toasterBot;
    public BncBot bncBot;

    public void ReturnObjectToPool(AEnemyController obj)
    {
        switch (obj)
        {
            case BabyBoxer _babyBoxer:
                babyBoxerQueue.Enqueue(_babyBoxer);
                break;

            case ToasterBot _toasterBot:
                toasterBotQueue.Enqueue(_toasterBot);
                break;

            case BncBot _bncBot:
                bncBotQueue.Enqueue(_bncBot);
                break;

            default:
                break;
        }
    }

    public void SpawnBabyBoxer(Vector2 spawnPos)
    {
        if(babyBoxerQueue.Count == 0)
        {
            BabyBoxer newBabyBoxer = Instantiate(babyBoxer, spawnPos, Quaternion.identity);
            babyBoxerQueue.Enqueue(newBabyBoxer);
        }

        babyBoxerQueue.Dequeue().Spawn(spawnPos);
    }

    public void SpawnToasterBot(Vector2 spawnPos)
    {
        if (toasterBotQueue.Count == 0)
        {
            ToasterBot newToasterBot = Instantiate(toasterBot, spawnPos, Quaternion.identity);
            toasterBotQueue.Enqueue(newToasterBot);
        }

        toasterBotQueue.Dequeue().Spawn(spawnPos);
    }

    public void SpawnBncBot(Vector2 spawnPos)
    {
        if (bncBotQueue.Count == 0)
        {
            BncBot newBncBot = Instantiate(bncBot, spawnPos, Quaternion.identity);
            bncBotQueue.Enqueue(newBncBot);
        }

        bncBotQueue.Dequeue().Spawn(spawnPos);
    }
}
