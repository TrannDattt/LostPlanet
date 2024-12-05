using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : AnimState
{
    [SerializeField] private float speedMultiplier;
    private float DashSpeed => Unit.stats.CurSpeed * speedMultiplier;

    public TrailRenderer dashTrail;

    public override void EnterState()
    {
        base.EnterState();

        dashTrail.emitting = true;
    }

    public override void ExitState()
    {
        base.ExitState();

        dashTrail.emitting = false;
    }

    public override void FixUpdateState()
    {
        base.FixUpdateState();

        ChangeBodyVelocity();
    }

    private void ChangeBodyVelocity()
    {
        Body.velocity = Unit.MoveDir * DashSpeed;
    }
}
