using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AUnitUI : MonoBehaviour
{
    protected AUnit unit;
    [SerializeField] protected Slider hpBar;
    //[SerializeField] TextMeshProUGUI coinCounter;

    public virtual void SetInstance(AUnit unit)
    {
        this.unit = unit;
    }

    public void UpdateHpBar()
    {
        hpBar.value = unit.stats.CurHealth / unit.stats.baseHealth;
    }
}
