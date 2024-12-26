using System;
using System.Collections;
using UnityEngine;

public abstract class ABaseState<EStateKey> where EStateKey : Enum
{
    public EStateKey StateKey { get; protected set; }
    public EStatus Status { get; protected set; }

    protected float startTime;
    protected float PlayedTime => Time.time - startTime;

    [SerializeField] protected float playSpeed = 1;
    [field : SerializeField] public float ResetStateTime { get; private set; }

    public abstract EStateKey GetNextState();
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixUpdateState();
    public abstract void ExitState();
    public abstract void OnTriggerEnter2D(Collider2D other);
    public abstract void OnTriggerStay2D(Collider2D other);
    public abstract void OnTriggerExit2D(Collider2D other);

    public IEnumerator ResetState()
    {
        yield return new WaitForSeconds(ResetStateTime);
        Status = EStatus.Ready;
    }
}

public enum EStatus
{
    Ready,
    Doing,
    Finished,
}