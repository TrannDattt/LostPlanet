using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : AUnit
{
    public GameObject Target { get; private set; }
    [field:SerializeField] public float TriggerRange { get; private set; } 
    [field:SerializeField] public float AttackRange { get; private set; }
    [field:SerializeField] public EEnemyType EnemyType { get; private set; }

    public bool InAttackRange => GetPlayerOffset().magnitude <= AttackRange;
    public bool InTriggerRange => GetPlayerOffset().magnitude <= TriggerRange;
    public bool CanAttack => InAttackRange && animStateMachine.StateDict[AnimStateMachine.EStateKey.Attack].Status == EStatus.Ready;

    public event Action Dying;

    private bool haveReturned;

    public override void Init()
    {
        gameObject.SetActive(true);
        haveReturned = false;

        base.Init();

        Target = Player.Instance.gameObject;
    }

    private void Update()
    {
        MoveDir = (!InAttackRange && InTriggerRange) ? GetPlayerOffset() : Vector2.zero;

        if(Status.CurHealth <= 0 && !haveReturned)
        {
            haveReturned = true;
            Dying?.Invoke();
            ReturnToPool();
        }
    }

    private Vector2 GetPlayerOffset()
    {
        return Target.transform.position - transform.position;
    }

    protected override void ChangeFaceDir()
    {
        if (GetPlayerOffset().x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(GetPlayerOffset().x), 1, 1);
        }
    }

    private async void ReturnToPool()
    {
        Player.Instance.Status.ChangeCoin(Status.CurCoinHave);
        Dying = null;

        await Task.Delay(1000);
        gameObject.SetActive(false);

        EnemyPooling.Instance.ReturnObjectToPool(this);
    }
}

public enum EEnemyType
{
    BabyBoxer,
    ToasterBot,
    BncBot,
    BotWheel,
    Guardian,
    MudGuard,
    SmallMons,
    StormHead,
    ZapBot,
}
