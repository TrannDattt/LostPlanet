using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Run : State
{
    public float runSpeed;

    public override void EnterState()
    {
        Animator.Play(clip.name);
        Completed = true;
    }

    public override void ExitState()
    {

    }

    public override void FixUpdateState()
    {
        ChangeBodyVelocity();
    }

    public override void UpdateState()
    {

    }

    private void ChangeBodyVelocity()
    {
        Body.velocity = core.MoveDir * runSpeed;
    }
}
