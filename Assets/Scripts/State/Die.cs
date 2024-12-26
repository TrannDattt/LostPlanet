using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimStateMachine;

[System.Serializable]
public class Die : AAnimState<EStateKey>
{
    //public Die(EStateKey stateKey, AUnit unit) : base(stateKey, unit) { }

    public override void EnterState()
    {
        unit.Core.animator.speed = playSpeed;
        unit.Core.animator.Play(clip.name);
        startTime = Time.time;
        Status = EStatus.Doing;
    }

    public override void ExitState() { }

    public override void FixUpdateState() { }

    public override EStateKey GetNextState() 
    {
        if (unit.Status.CurHealth > 0)
        {
            return EStateKey.Idle;
        }

        return EStateKey.Die;
    }

    public override void OnTriggerEnter2D(Collider2D other) { }

    public override void OnTriggerExit2D(Collider2D other) { }

    public override void OnTriggerStay2D(Collider2D other) { }

    public override void UpdateState() 
    {
        if(PlayedTime > clip.length / unit.Core.animator.speed)
        {
            Status = EStatus.Finished;
        }
    }
}
