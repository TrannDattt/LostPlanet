using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : AUnitUI
{
    [SerializeField] private TextMeshProUGUI coinCounter;
    [SerializeField] protected PopUpText coinText;

    protected override void OnEnable()
    {
        base.OnEnable();

        status.OnChangeCoin += PopUpCoinText;
        status.OnChangeCoin += UpdateCoinHave;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        status.OnChangeCoin -= PopUpCoinText;
        status.OnChangeCoin -= UpdateCoinHave;
    }

    public void UpdateCoinHave(int amount)
    {
        coinCounter.text = status.CurCoinHave.ToString();
    }

    public void PopUpCoinText(int amount)
    {
        TMPPooling.Instance.GetFromPool(unitDamageText, amount.ToString(), transform.position + .5f * Vector3.up);
    }
}
