using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyUI enemyUI;

    private float distanceToTarget;
    private UnitStats EnemyStats => enemy.stats;
    private UnitBehavior EnemyBehaviors => enemy.behaviors;
    private AnimState State => enemy.stateMachine.state;

    public delegate void UnitDie();
    public event UnitDie EnemyDie;

    public void Init(Vector2 spawnPos)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = spawnPos;
        enemy.core.body.simulated = true;

        enemy.SetInstance();
        enemyUI.SetInstance(enemy);
        SetState(EnemyBehaviors.idleState);
    }

    private void OnEnable()
    {
        enemy.DecreaseHealth += OnDecreaseHealth;
        EnemyDie += OnDie;
        EnemyDie += PlayerManager.Instance.PlayerUI.UpdateCoinHave;
    }

    private void OnDisable()
    {
        enemy.DecreaseHealth -= OnDecreaseHealth;
        EnemyDie = null;
    }

    public void SetState(AnimState _state, bool forceChange = false)
    {
        StartCoroutine(enemy.stateMachine.SetState(_state, forceChange));
    }

    private void SelectState()
    {
        if (EnemyStats.CurHealth <= 0)
        {
            EnemyDie?.Invoke();
            StartCoroutine(ReturnToPool());
            SetState(EnemyBehaviors.dieState, true);
            return;
        }

        //if (Input.GetKey(EnemyBehaviors.skillState.keyHold))
        //{
        //    SetState(EnemyBehaviors.skillState);
        //    return;
        //}

        if (distanceToTarget <= enemy.AttackRange && EnemyBehaviors.attackState.status == EStateStatus.Ready)
        {
            SetState(EnemyBehaviors.attackState);
            return;
        }

        if (distanceToTarget > enemy.AttackRange && distanceToTarget <= enemy.TriggerRange)
        {
            SetState(EnemyBehaviors.runState);
            return;
        }

        SetState(EnemyBehaviors.idleState);
    }

    private void ChangeFaceDir()
    {
        if (enemy.MoveDir.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(enemy.MoveDir.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    
    private void GetDistanceToTarget()
    {
        distanceToTarget = Vector2.Distance(enemy.Target.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        State?.UpdateBranchState();

        GetDistanceToTarget();
    }

    private void FixedUpdate()
    {
        State?.FixUpdateBranchState();

        SelectState();
        ChangeFaceDir();
    }

    public void OnDecreaseHealth(float amount)
    {
        if (EnemyBehaviors.hurtState.status == EStateStatus.Ready)
        {
            EnemyStats.ChangeHealth(-amount);
            enemyUI.UpdateHpBar();
            SetState(EnemyBehaviors.hurtState, true);
            TMPPooling.Instance.SpawnDamageTMP(transform.position, "-" + amount);
        }
    }

    public void OnDie()
    {
        PlayerManager.Instance.Player.stats.ChangeCoin(EnemyStats.CurCoinHave);
        if(EnemyStats.CurCoinHave > 0)
        {
            TMPPooling.Instance.SpawnCoinTMP(enemy.Target.transform.position, EnemyStats.CurCoinHave.ToString());
            EnemyStats.LostAllCoin();
        }
    }

    private IEnumerator ReturnToPool()
    {
        enemy.core.body.simulated = false;

        yield return new WaitForSeconds(1);
        enemyUI.ReturnHpBarToPool();
        gameObject.SetActive(false);
        EnemyPooling.Instance.ReturnObjectToPool(enemy);
    }
}
