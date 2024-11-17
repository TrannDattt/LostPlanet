using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected AController core;

    protected Animator Animator => core.animator;
    protected Rigidbody2D Body => core.body;
    protected CharacterAudio CharacterAudio => core.characterAudio;

    public AnimationClip clip;
    //public float playSpeed = 1;
    float startTime;
    public float Time => UnityEngine.Time.time - startTime;

    public bool Completed {  get; protected set; }

    public StateMachine stateMachine;
    protected StateMachine parent;
    protected State ChildState => stateMachine.state;

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
        startTime = UnityEngine.Time.time;
        Completed = false;
    }

    public void SetCore(AController _core)
    {
        this.core = _core;
    }

    protected void SetState(State _state, bool forceReset = false)
    {
        stateMachine.SetState(_state, forceReset);
    }

    private void Awake()
    {
        Completed = true;
    }

    public void UpdateBranchState()
    {
        UpdateState();
        if (ChildState)
        {
            ChildState.UpdateBranchState();
        }
    }

    public void FixUpdateBranchState()
    {
        FixUpdateState();
        if (ChildState)
        {
            ChildState.FixUpdateBranchState();
        }
    }
}
