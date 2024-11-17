using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AProjectile
{
    public override void Fly()
    {
        base.Fly();

        //body.MovePosition(flySpeed * Time.deltaTime * targetPos);
    }
}
