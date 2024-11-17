using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
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
        Body.velocity = Vector2.zero;
    }

    public override void UpdateState()
    {

    }
}
