using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack : State
{
    public float damage => core.damage;

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
        animator.speed = playSpeed;
        float _time = Helpers.Map(time, 0, 1, 0, animator.speed, true);

        body.velocity = Vector2.zero;

        animator.Play(clip.name, 0, _time);

        if (time >= clip.length / animator.speed)
        {
            completed = true;

            core.norAttacking = false;
            //if (core.heavyAttacking)
            //{
                core.heavyAttacking = false;
            //}
        }
    }
}
