using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : AUnit
{
    [field:SerializeField] public EProjectileType ProjectileType { get; private set; }
    [field:SerializeField] public EProjFlyType FlyType { get; private set; }

    public override void SetInstance()
    {
        base.SetInstance();
    }
}

public enum EProjFlyType
{
    Flip,
    Rotate,
}

public enum EProjectileType
{
    Grenade,
    Beam,
    Bullet,
}
