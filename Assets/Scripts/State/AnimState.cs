using System.Collections;
using UnityEngine;

public abstract class AnimState : MonoBehaviour
{
    public AUnit Unit {  get; protected set; }
    protected Animator Animator => Unit.core.animator;
    protected Rigidbody2D Body => Unit.core.body;
    protected UnitAudio CharacterAudio => Unit.core.unitAudio;

    public AnimationClip clip;

    float startTime;
    public float Time => UnityEngine.Time.time - startTime;

    public float timeBeforeCanChangeState;
    public float transitionTime;

    public float playSpeed = 1;
    public float resetStateTime;

    public EStateStatus status;

    public StateMachine StateMachine;
    protected StateMachine parent;
    protected AnimState ChildState => StateMachine.state;

    public virtual void EnterState()
    {
        if (clip != null)
        { 
            Animator.Play(clip.name);
        }

        //StartCoroutine(CountDownChangeTime());
    }

    public virtual void UpdateState()
    {

    }

    public virtual void FixUpdateState()
    {

    }

    public virtual void ExitState()
    {
        StartCoroutine(ResetState());
    }

    private IEnumerator ResetState()
    {
        yield return new WaitForSeconds(resetStateTime);
        status = EStateStatus.Ready;
    }

    public void InitialState()
    {
        StateMachine = new StateMachine();

        startTime = UnityEngine.Time.time;
        Animator.speed = playSpeed;
        status = EStateStatus.Playing;
    }

    protected void InitialChildStates(params AnimState[] states)
    {
        foreach (var state in states)
        {
            state.SetCore(Unit);
        }
    }

    public void SetCore(AUnit _core)
    {
        this.Unit = _core;
    }

    protected void SetState(AnimState _state, bool forceChange = false)
    {
        StartCoroutine(StateMachine.SetState(_state, forceChange));
    }

    public void UpdateBranchState()
    {
        UpdateState();
        if (ChildState)
        {
            ChildState.UpdateBranchState();
        }

        CheckFinishState();
    }

    protected virtual void CheckFinishState()
    {
        if(Time > timeBeforeCanChangeState)
        {
            status = EStateStatus.Finish;
        }
    }

    private IEnumerator CountDownChangeTime()
    {
        yield return new WaitForSeconds(timeBeforeCanChangeState);
        status = EStateStatus.Finish;
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

public enum EStateStatus
{
    Ready,
    Playing,
    Finish,
}
