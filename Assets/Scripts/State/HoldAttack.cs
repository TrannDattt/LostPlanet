using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAttack : AnimState
{
    public override void UpdateState()
    {
        base.UpdateState();

        float _time = Helpers.Map(Time, 0, clip.length / Animator.speed, 0, 0.9f, true);
        Animator.Play(clip.name, 0, _time);
    }
}
