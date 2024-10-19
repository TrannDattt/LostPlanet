using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInit : State
{
    public override void EnterState()
    {
        animator.Play(clip.name);
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
        if(time >= clip.length / animator.speed)
        {
            completed = true;
            Destroy(core.gameObject);
        }
    }
}
