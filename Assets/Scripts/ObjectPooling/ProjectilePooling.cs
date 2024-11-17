using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ProjectilePooling : Singleton<ProjectilePooling>
{
    private Queue<Grenade> grenadeQueue = new Queue<Grenade>();
    private Queue<Beam> beamQueue = new Queue<Beam>();
    private Queue<Bullet> bulletQueue = new Queue<Bullet>();

    public Grenade grenade;
    public Beam beam;
    public Bullet bullet;

    public void ReturnObjectToPool(AProjectile projectile)
    {
        switch (projectile)
        {
            case Grenade _grenade:
                grenadeQueue.Enqueue(_grenade);
                break;

            case Beam _beam:
                beamQueue.Enqueue(_beam);
                break;

            case Bullet _bullet:
                bulletQueue.Enqueue(_bullet);
                break;

            default:
                break;
        }
    }

    public void FireGrenade(Transform source, Vector2 targetPos)
    {
        if(grenadeQueue.Count == 0)
        {
            Grenade newGrenade = Instantiate(grenade, source.position, Quaternion.identity);
            grenadeQueue.Enqueue(newGrenade);
        }

        grenadeQueue.Dequeue().SetInstance(source, targetPos);
    }

    public void FireBullet(Vector2 spawnPos, Vector2 targetPos)
    {

    }

    public void FireBeam(Transform source, Vector2 targetPos)
    {
        if (beamQueue.Count == 0)
        {
            Beam newBeam = Instantiate(beam, source.position, Quaternion.identity);
            beamQueue.Enqueue(newBeam);
        }

        beamQueue.Dequeue().SetInstance(source, targetPos);
    }
}
