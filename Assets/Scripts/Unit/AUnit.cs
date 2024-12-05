using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AUnit : MonoBehaviour
{
    public UnitCore core; 
    public UnitStats stats;
    public UnitBehavior behaviors;
    public Vector2 MoveDir { get; protected set; }

    public StateMachine stateMachine;

    public EUnitType unitType;

    public virtual void SetInstance()
    {
        stats.SetUp();
        behaviors.SetUp(this);

        stateMachine = new StateMachine();
    }
}

public enum EUnitType
{
    Player,
    Enemy,
    NPC,
    Projectile,
}

[System.Serializable]
public class UnitStats
{
    public float baseHealth;
    public float baseDamage;
    public float baseSpeed;
    public int startCoinHave;

    public float CurHealth { get; private set; }
    public float CurDamage { get; private set; }
    public float CurSpeed { get; private set; }
    public int CurCoinHave { get; private set; }

    public void SetUp()
    {
        CurHealth = baseHealth;
        CurDamage = baseDamage;
        CurSpeed = baseSpeed;
        CurCoinHave = startCoinHave;
    }

    public void ChangeHealth(float amount) => CurHealth = Mathf.Clamp(CurHealth + amount, 0, baseHealth);
    public void LostAllCoin() => CurCoinHave = 0;
    public void ChangeCoin(int amount) => CurCoinHave = Mathf.Clamp(CurCoinHave + amount, 0, 99999);
}

[System.Serializable]
public class UnitBehavior
{
    public Idle idleState;
    public Run runState;
    public Dash dashState;
    public Attack attackState;
    public Skill skillState;
    public Hurt hurtState;
    public Die dieState;

    public void SetUp(AUnit unit)
    {
        idleState?.SetCore(unit);
        runState?.SetCore(unit);
        dashState?.SetCore(unit);
        attackState?.SetCore(unit);
        skillState?.SetCore(unit);
        hurtState?.SetCore(unit);
        dieState?.SetCore(unit);
    }
}

[System.Serializable]
public class UnitCore
{
    public Rigidbody2D body;
    public Animator animator;
    public UnitAudio unitAudio;
}
