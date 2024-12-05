using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : AUnit
{
    public GameObject Target { get; private set; }

    [field: SerializeField] public EEnemyType EnemyType { get; private set; }
    [field:SerializeField] public float TriggerRange { get; private set; } 
    [field:SerializeField] public float AttackRange { get; private set; }

    public delegate void ChangeHealth(float amount);
    public event ChangeHealth DecreaseHealth;
    public event ChangeHealth IncreaseHealth;

    public override void SetInstance()
    {
        base.SetInstance();

        Target = PlayerManager.Instance.gameObject;
    }

    private void FixedUpdate()
    {
        MoveDir = (Target.transform.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent<Attack>(out Attack attack))
            {
                if (attack.gameObject.CompareTag("PlayerAttack"))
                {
                    DecreaseHealth(attack.DamageDo);
                }
            }
            else if (collision.gameObject.TryGetComponent<Skill>(out Skill skill))
            {
                if (skill.gameObject.CompareTag("PlayerAttack"))
                {
                    DecreaseHealth(skill.DamageDo);
                }
            }
        }
    }
}

public enum EEnemyType
{
    BabyBoxer,
    ToasterBot,
    BnCBot,
    BotWheel,
    Guardian,
    MudGuard,
    SmallMons,
    StormHead,
    ZapBot,
}
