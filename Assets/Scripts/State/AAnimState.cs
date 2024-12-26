using System;
using UnityEngine;

public abstract class AAnimState<EStateKey> : ABaseState<EStateKey> where EStateKey : Enum
{
    [SerializeField] protected AnimationClip clip;

    protected AUnit unit;

    public void Init(EStateKey stateKey, AUnit unit)
    {
        this.unit = unit;
        StateKey = stateKey;
        Status = EStatus.Ready;
    }
}
