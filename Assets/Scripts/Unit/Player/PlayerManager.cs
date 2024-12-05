using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [field:SerializeField] public Player Player {  get; private set; }
    [field:SerializeField] public PlayerUI PlayerUI { get; private set; }

    private UnitStats PlayerStats => Player.stats;
    private UnitBehavior PlayerBehavior => Player.behaviors;
    private AnimState State => Player.stateMachine.state;

    private void Init()
    {
        Player.SetInstance();
        PlayerUI.SetInstance(Player);

        Player.DecreaseHealth += OnDecreaseHealth;
        SetState(PlayerBehavior.idleState);
    }

    public void SetState(AnimState _state, bool forceChange = false)
    {
        StartCoroutine(Player.stateMachine.SetState(_state, forceChange));
    }

    private void SelectState()
    {
        if (PlayerStats.CurHealth <= 0)
        {
            StartCoroutine(CharacterDie());
            SetState(PlayerBehavior.dieState, true);
            return;
        }

        if (Input.GetKey(PlayerBehavior.skillState.keyHold))
        {
            SetState(PlayerBehavior.skillState);
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetState(PlayerBehavior.dashState);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SetState(PlayerBehavior.attackState);
            return;
        }

        if (!Mathf.Approximately(Player.MoveDir.magnitude, 0))
        {
            SetState(PlayerBehavior.runState);
            return;
        }

        SetState(PlayerBehavior.idleState);
    }

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Update()
    {
        State?.UpdateBranchState();

        SelectState();
    }

    private void FixedUpdate()
    {
        State?.FixUpdateBranchState();

        ChangeFaceDir();
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1);
        ServiceLocator.Instance.PlayerDie();
    }

    private void ChangeFaceDir()
    {
        if (Player.MoveDir.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(Player.MoveDir.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private IEnumerator CharacterDie()
    {
        Player.DecreaseHealth -= OnDecreaseHealth;
        PlayerBehavior.dieState.status = EStateStatus.Finish;
        StartCoroutine(StopGame());

        yield return null;
    }

    public void OnDecreaseHealth(float amount)
    {
        if (PlayerBehavior.hurtState.status == EStateStatus.Ready)
        {
            PlayerStats.ChangeHealth(-amount);
            PlayerUI.UpdateHpBar();
            SetState(PlayerBehavior.hurtState, true); 
        }
    }
}
