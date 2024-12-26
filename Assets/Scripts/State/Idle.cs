using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static AnimStateMachine;

[System.Serializable]
public class Idle : AAnimState<EStateKey>
{
    //public Idle(EStateKey stateKey, AUnit unit) : base(stateKey, unit) { }

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
        if (!Mathf.Approximately(unit.MoveDir.magnitude, 0))
        {
            return EStateKey.Run;
        }

        if (unit is Player && Input.GetKeyDown(KeyCode.Mouse0) || unit is Enemy enemy && enemy.CanAttack)
        {
            return EStateKey.Attack;
        }

        return EStateKey.Idle;
    }

    public override void OnTriggerEnter2D(Collider2D other) { }

    public override void OnTriggerExit2D(Collider2D other) { }

    public override void OnTriggerStay2D(Collider2D other) { }

    public override void UpdateState() => Status = EStatus.Finished;
}
