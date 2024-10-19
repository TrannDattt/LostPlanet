using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ACoinManager : MonoBehaviour
{
    private Core core;
    public TextMeshProUGUI coinTMPU;

    protected int coinHave => core.coinHave;

    public void SetCore(Core core)
    {
        this.core = core;
    }

    public void SetCoinText()
    {
        coinTMPU.text = coinHave.ToString();
    }
}
