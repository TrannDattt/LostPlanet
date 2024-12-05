using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSkill : AnimState
{
    public float damageMultiplier;

    public override void FixUpdateState()
    {
        base.FixUpdateState();

        Body.velocity = Vector3.zero;
    }
}
