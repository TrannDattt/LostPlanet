using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ProjectilePooling : Singleton<ProjectilePooling>
{
    private Queue<Projectile> grenadeQueue = new();
    private Queue<Projectile> beamQueue = new();
    private Queue<Projectile> bulletQueue = new();

    [SerializeField] private Projectile grenade;
    [SerializeField] private Projectile beam;
    [SerializeField] private Projectile bullet;

    public void ReturnObjectToPool(Projectile projectile)
    {
        switch (projectile.ProjectileType)
        {
            case EProjectileType.Grenade:
                grenadeQueue.Enqueue(projectile);
                break;

            case EProjectileType.Beam:
                beamQueue.Enqueue(projectile);
                break;

            case EProjectileType.Bullet:
                bulletQueue.Enqueue(projectile);
                break;
        }
    }

    public void FireProjectile(Projectile projectile, Transform source, Vector2 flyDir)
    {
        switch (projectile.ProjectileType)
        {
            case EProjectileType.Grenade:
                if (grenadeQueue.Count == 0)
                {
                    var newGrenade = Instantiate(grenade, source.position, Quaternion.identity);
                    grenadeQueue.Enqueue(newGrenade);
                }

                grenadeQueue.Dequeue().gameObject.GetComponent<ProjectileManager>().Init(source, flyDir);
                break;

            case EProjectileType.Beam:
                if (beamQueue.Count == 0)
                {
                    var newBeam = Instantiate(beam, source.position, Quaternion.identity);
                    beamQueue.Enqueue(newBeam);
                }

                beamQueue.Dequeue().gameObject.GetComponent<ProjectileManager>().Init(source, flyDir);
                break;

            case EProjectileType.Bullet:
                if (bulletQueue.Count == 0)
                {
                    var newBullet = Instantiate(bullet, source.position, Quaternion.identity);
                    bulletQueue.Enqueue(newBullet);
                }

                bulletQueue.Dequeue().gameObject.GetComponent<ProjectileManager>().Init(source, flyDir);
                break;
        }
    }
}
