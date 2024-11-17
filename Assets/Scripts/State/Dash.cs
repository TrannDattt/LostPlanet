using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : State
{
    public float dashSpeed;
    public TrailRenderer dashTrail;
    public float resetDashTime;

    public override void EnterState()
    {
        Animator.Play(clip.name);
        dashTrail.emitting = true;
    }

    public override void ExitState()
    {
        
    }

    public override void FixUpdateState()
    {

    }

    public override void UpdateState()
    {

        if (!Completed)
        {
            Dashing();
        }

        if (Time > clip.length) 
        {
            StartCoroutine(ResetDash());
            dashTrail.emitting = false;
            Completed = true;
        }
    }

    private void Dashing()
    {
        Body.velocity = core.MoveDir * dashSpeed;
    }

    private IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(resetDashTime);
        core.canDash = true;
    }
}
