using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : State
{
    public float chargeTime;

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
        animator.speed = 1 / chargeTime;
        float _time = Helpers.Map(time, 0, 1, 0, animator.speed, true);

        animator.Play(clip.name, 0, _time);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            completed = true;
            core.charging = false;
        }    
    }
}
