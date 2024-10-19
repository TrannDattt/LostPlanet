using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    public float runSpeed;

    public override void EnterState()
    {
        animator.Play(clip.name);
    }

    public override void ExitState()
    {

    }

    public override void FixUpdateState()
    {

    }

    public override void UpdateState()
    {
        float _speed = Helpers.Map(body.velocity.magnitude, 0, runSpeed, 0, 1, false);
        animator.speed = _speed;

        if (core.norAttacking)
        {
            completed = true;
        }
    }
}
