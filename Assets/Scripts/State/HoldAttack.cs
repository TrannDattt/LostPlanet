using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAttack : State
{
    public KeyCode keyHold;

    public override void EnterState()
    {
        //Animator.Play(clip.name);
        Completed = true;
    }

    public override void ExitState()
    {

    }

    public override void FixUpdateState()
    {

    }

    public override void UpdateState()
    {
        float _time = Helpers.Map(Time, 0, clip.length / Animator.speed, 0, 0.9f, true);
        Animator.Play(clip.name, 0, _time);
    }
}
