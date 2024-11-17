using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInit : State
{
    public override void EnterState()
    {
        Animator.Play(clip.name);
    }

    public override void ExitState()
    {
        Destroy(core.gameObject);
    }

    public override void FixUpdateState()
    {
        
    }

    public override void UpdateState()
    {
        if(Time >= clip.length / Animator.speed)
        {
            Completed = true;
            Destroy(core.gameObject);
        }
    }
}
