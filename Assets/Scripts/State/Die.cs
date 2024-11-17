using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : State
{
    public override void EnterState()
    {
        Animator.Play(clip.name);
    }

    public override void ExitState()
    {

    }

    public override void FixUpdateState()
    {

    }

    public override void UpdateState()
    {
        Animator.speed = 1;
        float _time = Helpers.Map(Time, 0, 1, 0, Animator.speed, true);

        Animator.Play(clip.name, 0, _time);
    }
}
