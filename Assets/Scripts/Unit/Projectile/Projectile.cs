using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private UnitCore unitCore;
    [SerializeField] private float speed;
    [SerializeField] private float existTime = 1f;

    private float timeCounter = 0;
    private GameObject target;
    private Vector2 moveDir;

    public event Action<AUnit> OnDoingDamage;

    [field : SerializeField] public EProjectileType ProjectileType { get; private set; }

    private bool haveReturned;

    public void Init()
    {
        gameObject.SetActive(true);
        haveReturned = false;
        timeCounter = 0;

        target = Player.Instance.gameObject;
        moveDir = GetPlayerOffset().normalized;

        switch (ProjectileType)
        {
            case EProjectileType.Grenade:
                ChangeFaceDir();
                break;

            case EProjectileType.Bullet:
                RotateProjectile();
                break;

            case EProjectileType.Beam:
                RotateProjectile();
                break;

            default:
                break;
        }

        unitCore.animator.SetTrigger("Attack");
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > existTime && !haveReturned)
        {
            haveReturned = true;
            ReturnToPool();
        }
    }

    protected void FixedUpdate()
    {
        unitCore.body.position = Vector2.MoveTowards(unitCore.body.position, unitCore.body.position + moveDir, speed * Time.fixedDeltaTime);
    }

    private void ChangeFaceDir()
    {
        if(!Mathf.Approximately(moveDir.x, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDir.x), 1, 1);
        }
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);

        ProjectilePooling.Instance.ReturnObjectToPool(this);
    }

    private Vector2 GetPlayerOffset()
    {
        return target.transform.position - transform.position;
    }

    private void RotateProjectile()
    {
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        unitCore.body.MoveRotation(angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<AUnit>(out AUnit damagedUnit))
        { 
            OnDoingDamage?.Invoke(damagedUnit);
        }
    }

    private void OnDisable()
    {
        OnDoingDamage = null;
    }
}

public enum EProjectileType
{
    Grenade,
    Beam,
    Bullet,
}
