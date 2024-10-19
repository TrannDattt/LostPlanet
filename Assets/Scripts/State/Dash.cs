using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : State
{
    public float dashSpeed;

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
        animator.speed = 2.5f;

        float _time = Helpers.Map(time, 0, 1, 0, animator.speed, true);
        animator.Play(clip.name, 0, _time);

        if (time >= clip.length / animator.speed)
        {
            completed = true;
            core.dashing = false;
        }
    }
}
