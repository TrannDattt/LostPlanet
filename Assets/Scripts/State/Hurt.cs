using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static AnimStateMachine;

[System.Serializable]
public class Hurt : AAnimState<EStateKey>
{
    //public Hurt(EStateKey stateKey, AUnit unit) : base(stateKey, unit) { }

    public override void EnterState()
    {
        unit.Core.animator.speed = playSpeed;
        unit.Core.animator.Play(clip.name);
        startTime = Time.time;
        Status = EStatus.Doing;
    }
    public override void ExitState() { }

    public override void FixUpdateState() => unit.Core.body.velocity = Vector2.zero;

    public override EStateKey GetNextState()
    {
        if(unit.Status.CurHealth <= 0)
        {
            Status = EStatus.Finished;
            return EStateKey.Die;
        }

        if(Status == EStatus.Finished)
        {
            return EStateKey.Idle;
        }

        return EStateKey.Hurt;
    }

    public override void OnTriggerEnter2D(Collider2D other) { }

    public override void OnTriggerExit2D(Collider2D other) { }

    public override void OnTriggerStay2D(Collider2D other) { }

    public override void UpdateState()
    {
        if (PlayedTime > clip.length / unit.Core.animator.speed)
        {
            Status = EStatus.Finished;
        }
    }
}
