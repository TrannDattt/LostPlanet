using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeam : AProjectile
{
    protected override void Start()
    {
        RotateProjectile();
    }

    void RotateProjectile()
    {
        Vector2 direction = destinatePos - spawnPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
