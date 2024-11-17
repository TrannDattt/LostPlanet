using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : State
{
    public float resetHurtTime;

    public override void EnterState()
    {
        Animator.Play(clip.name);
        //CharacterAudio.PlayHurtSound();
    }

    public override void ExitState()
    {
        //core.hurting = false;
    }

    public override void FixUpdateState()
    {

    }

    public override void UpdateState()
    {
        if (Time >= clip.length / Animator.speed)
        {
            core.hurting = false;
            StartCoroutine(ResetHurt());
            Completed = true;
        }
    }

    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(resetHurtTime);
        core.canHurt = true;
    }
}
