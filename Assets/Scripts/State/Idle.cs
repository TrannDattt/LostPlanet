using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : AnimState
{
    public override void FixUpdateState()
    {
        base.FixUpdateState();

        Body.velocity = Vector2.zero;
    }
}
