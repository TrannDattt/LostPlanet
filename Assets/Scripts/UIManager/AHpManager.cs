using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AHpManager : MonoBehaviour 
{
    private Core core;
    public Slider hpSlider;

    protected float healthPercentage => core.curHealth / core.maxHealth;

    public void SetCore(Core core)
    {
        this.core = core;
    }

    public void SetHpBar()
    {
        hpSlider.value = healthPercentage;
    }
}
