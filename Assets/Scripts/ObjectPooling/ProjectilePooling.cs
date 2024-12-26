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

    private Projectile GetProjectile(Projectile projectile, Queue<Projectile> projectileQueue, Vector2 spawnPos)
    {
        Projectile spawnedProjectile;

        if (projectileQueue.Count == 0)
        {
            spawnedProjectile = Instantiate(projectile, spawnPos, Quaternion.identity);
        }
        else
        {
            spawnedProjectile = projectileQueue.Dequeue();
        }

        spawnedProjectile.transform.localPosition = spawnPos;
        spawnedProjectile.Init();
        return spawnedProjectile;
    }

    public Projectile GetFromPool(Projectile projectile, Vector2 spawnPos)
    {
        return projectile.ProjectileType switch
        {
            EProjectileType.Grenade => GetProjectile(grenade, grenadeQueue, spawnPos),
            EProjectileType.Beam => GetProjectile(beam, beamQueue, spawnPos),
            EProjectileType.Bullet => GetProjectile(bullet, bulletQueue, spawnPos),
            _ => null,
        };
    }

    private void ResetPool()
    {
        beamQueue = new();
        bulletQueue = new();
        grenadeQueue = new();
    }

    private void Start()
    {
        GameManager.Instance.OnLevelStarted += ResetPool;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelStarted -= ResetPool;
    }
}
