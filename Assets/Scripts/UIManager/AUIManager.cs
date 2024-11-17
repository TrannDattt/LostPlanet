using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AUIManager : MonoBehaviour
{
    public AController core;

    public AHpManager hpManager;
    //public AMpManager mpManager;
    public ACoinManager coinManager;
    public AStatusManager statusManager;

    void Awake()
    {
        hpManager.SetCore(core);
        coinManager?.SetCore(core);
    }

    // Update is called once per frame
    void Update()
    {
        hpManager.SetHpBar();
        coinManager?.SetCoinText();
    }
}
