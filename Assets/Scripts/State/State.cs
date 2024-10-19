using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Core core;

    protected Animator animator => core.animator;
    protected Rigidbody2D body => core.body;

    public AnimationClip clip;
    public float playSpeed = 1;
    float startTime;
    public float time => Time.time - startTime;

    public bool completed {  get; protected set; }

    public StateMachine stateMachine;
    protected StateMachine parent;
    protected State state => stateMachine.state;

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void FixUpdateState();

    public abstract void ExitState();

    public void SetParent(StateMachine _parent)
    {
        this.parent = _parent;
    }

    public void InitialState()
    {
        stateMachine = new StateMachine();
        startTime = Time.time;
        completed = false;
    }

    public void SetCore(Core _core)
    {
        this.core = _core;
    }

    protected void SetState(State _state, bool forceReset = false)
    {
        stateMachine.SetState(_state, forceReset);
    }

    private void Awake()
    {
        completed = true;
    }

    public void UpdateBranchState()
    {
        UpdateState();
        if (state)
        {
            state.UpdateBranchState();
        }
    }

    public void FixUpdateBranchState()
    {
        FixUpdateState();
        if (state)
        {
            state.FixUpdateBranchState();
        }
    }
}
