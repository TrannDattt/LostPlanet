using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : AUnitUI
{
    [SerializeField] private TextMeshProUGUI coinCounter;

    public void UpdateCoinHave()
    {
        coinCounter.text = unit.stats.CurCoinHave.ToString();
    }
}
