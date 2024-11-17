using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack : State
{
    public AProjectile projectile;

    public float damage;
    public float knockBackForce = 1;

    public override void EnterState()
    {
        Animator.Play(clip.name);

        if (projectile != null)
        {
            switch (projectile)
            {
                case Grenade:
                    ProjectilePooling.Instance.FireGrenade(core.gameObject.transform, core.target.transform.position);
                    break;

                case Beam:
                    ProjectilePooling.Instance.FireBeam(core.gameObject.transform, core.target.transform.position);
                    break;

                case Bullet:
                    break;

                default:
                    break;
            }
        }
    }

    public override void ExitState()
    {

    }

    public override void FixUpdateState()
    {

    }

    public override void UpdateState()
    {
        Body.velocity = Vector2.zero;

        if (Time >= clip.length)
        {
            Completed = true;
        }
    }
}
