using System;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    private AUnit unit;

    public float baseHealth;
    public float baseDamage;
    public float baseSpeed;
    public int startCoinHave;

    public event Action<float> OnChangeCurHealth;
    public event Action<int> OnChangeCoin;

    public float CurHealth { get; private set; }
    public float CurDamage { get; private set; }
    public float CurSpeed { get; private set; }
    public int CurCoinHave { get; private set; }

    public void Init(AUnit unit)
    {
        this.unit = unit;

        CurHealth = baseHealth;
        CurDamage = baseDamage;
        CurSpeed = baseSpeed;
        CurCoinHave = startCoinHave;

        OnChangeCurHealth?.Invoke(baseHealth - CurHealth);
        OnChangeCoin?.Invoke(startCoinHave - CurCoinHave);
    }

    public void ChangeCurHealth(float amount) 
    {
        CurHealth = Mathf.Clamp(CurHealth + amount, 0, baseHealth);
        OnChangeCurHealth?.Invoke(amount);
    }

    public void ChangeMaxHealth(float amount)
    {
        baseHealth += amount;
        //OnChangeMaxHealth?.Invoke(amount);
    }

    public void ChangeCurDamage(float amountPer) => CurDamage *= amountPer;
    public void ChangeMaxDamage(float amountPer) => baseDamage *= amountPer;
    public void ChangeCurSpeed(float amountPer) => CurSpeed *= amountPer;
    public void ChangeMaxSpeed(float amountPer) => baseSpeed *= amountPer;
    public void ChangeCoin(int amount)
    {
        CurCoinHave = Mathf.Clamp(CurCoinHave + amount, 0, 99999);
        OnChangeCoin?.Invoke(amount);
    }

    public void UpgradeStatus(Upgrade upgrade)
    {
        switch (upgrade.UpgradeType)
        {
            case Upgrade.EUpgrade.MaxHealth:
                ChangeMaxHealth(upgrade.Amount * baseHealth);
                break;

            case Upgrade.EUpgrade.Damage:
                ChangeMaxDamage(1 + upgrade.Amount);
                CurDamage = baseDamage;
                break;

            case Upgrade.EUpgrade.Speed:
                ChangeMaxSpeed(1 + upgrade.Amount);
                CurSpeed = baseSpeed;
                break;

            case Upgrade.EUpgrade.Recover:
                ChangeCurHealth(upgrade.Amount * baseHealth);
                break;

            default:
                break;
        }
        ChangeCoin(-1 * upgrade.Price);
    }
}
