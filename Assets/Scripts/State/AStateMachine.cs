using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AStateMachine<EStateKey> : MonoBehaviour where EStateKey : Enum
{
    public ABaseState<EStateKey> CurState {  get; protected set; }
    public Dictionary<EStateKey, ABaseState<EStateKey>> StateDict { get; protected set; } = new();

    private bool isInTransition = false;

    protected void AddState(EStateKey key, ABaseState<EStateKey> newState)
    {
        if(!StateDict.TryAdd(key, newState))
        {
            StateDict[key] = newState;
        }
    }

    private void Update()
    {
        EStateKey nextStateKey = CurState.GetNextState();

        if(!isInTransition)
        {
            if (!nextStateKey.Equals(CurState.StateKey))
            {
                isInTransition = true;
                TransitToState(nextStateKey);
            }

            CurState.UpdateState();
        }
    }

    private void FixedUpdate()
    {
        CurState.FixUpdateState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurState.OnTriggerEnter2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CurState.OnTriggerStay2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CurState.OnTriggerExit2D(collision);
    }

    public void TransitToState(EStateKey stateKey)
    {
        if ((CurState == null || CurState.Status == EStatus.Finished) && StateDict[stateKey].Status == EStatus.Ready)
        {
            if (CurState != null)
            {
                CurState.ExitState();
                StartCoroutine(CurState.ResetState());
            }
            CurState = StateDict[stateKey];
            CurState.EnterState();
        }
        isInTransition = false;
    }
}
