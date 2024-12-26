using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateMachine : AStateMachine<AnimStateMachine.EStateKey>
{
    public enum EStateKey
    {
        Idle,
        Run,
        Attack,
        Hurt,
        Die,
    }

    private AUnit unit;

    [SerializeField] private Idle idle;
    [SerializeField] private Run run;
    [SerializeField] private Attack attack;
    [SerializeField]private Hurt hurt;
    [SerializeField] private Die die;


    public void Init(AUnit unit)
    {
        this.unit = unit;

        idle.Init(EStateKey.Idle, unit);
        run.Init(EStateKey.Run, unit);
        attack.Init(EStateKey.Attack, unit);
        hurt.Init(EStateKey.Hurt, unit);
        die.Init(EStateKey.Die, unit);

        AddState(EStateKey.Idle, idle);
        AddState(EStateKey.Run, run);
        AddState(EStateKey.Attack, attack);
        AddState(EStateKey.Hurt, hurt);
        AddState(EStateKey.Die, die);

        //StateDict.TryAdd(EStateKey.Idle, idle);
        //StateDict.TryAdd(EStateKey.Run, run);
        //StateDict.TryAdd(EStateKey.Attack, attack);
        //StateDict.TryAdd(EStateKey.Hurt, hurt);
        //StateDict.TryAdd(EStateKey.Die, die);

        TransitToState(EStateKey.Idle);
    }
}
