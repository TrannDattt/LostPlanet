using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Run : AnimState
{
    public float RunSpeed => Unit.stats.CurSpeed;

    public override void FixUpdateState()
    {
        base.FixUpdateState();

        ChangeBodyVelocity();
    }

    private void ChangeBodyVelocity()
    {
        Body.velocity = Unit.MoveDir * RunSpeed;
    }
}
