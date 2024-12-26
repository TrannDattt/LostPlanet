using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AUnitUI : MonoBehaviour
{
    protected UnitStatus status;
    [SerializeField] protected Image hpBar;
    [SerializeField] protected PopUpText unitDamageText;

    protected virtual void Awake()
    {
        status = GetComponent<UnitStatus>();
    }

    protected virtual void OnEnable()
    {
        status.OnChangeCurHealth += UpdateHpBar;
        status.OnChangeCurHealth += PopUpDamageText;
    }

    protected virtual void OnDisable()
    {
        status.OnChangeCurHealth -= UpdateHpBar;
        status.OnChangeCurHealth -= PopUpDamageText;
    }

    public void UpdateHpBar(float amount)
    {
        hpBar.fillAmount = status.CurHealth / status.baseHealth;
    }

    public void PopUpDamageText(float amount)
    {
        TMPPooling.Instance.GetFromPool(unitDamageText, amount.ToString(), transform.position + .5f * Vector3.up);
    }
}
