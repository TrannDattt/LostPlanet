using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EnemyController : Core
{
    // State
    public Idle idleState;
    public Run runState;
    public Dash dashState;
    public NormalAttack normalAttackState;
    public RangeAttack rangeAttackState;
    public ChargeAttack chargeAttackState;
    public ReleaseChargeAttack releaseChargeAttackState;
    public Hurt hurtState;
    public Die dieState;

    public bool rangedAttack = false;

    //For ranged attack
    public GameObject projectileObject;

    public GameObject target => GameObject.Find("Player");
    Vector2 direction => target.transform.position - body.transform.position;

    public float triggerDistance;
    float curDistance => Vector2.Distance(target.transform.position, body.transform.position);

    bool canDash = true;
    public float dashCdTime;

    bool canAttack = true;
    public float attackCdTime;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        SetInstance();

        SetState(idleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        GetStatus();
        ChoseState();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void ChasingTarget()
    {
        body.velocity = (curDistance > attackRange) ?
            direction * runState.runSpeed / Mathf.Max(Mathf.Abs(direction.x), Mathf.Abs(direction.y)) :
            Vector2.zero;

        if (direction.x != 0)
        {
            body.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }
    }



    void AttackingTarget()
    {
        norAttacking = true;

        if (rangedAttack)
        {
            GameObject spawnProjectileObject =  Instantiate(projectileObject, 
                (Vector2)body.transform.position + Mathf.Sign(direction.x) * new Vector2(0.5f, 0), 
                Quaternion.identity);

            AProjectile projectile = spawnProjectileObject.GetComponent<AProjectile>();
            projectile.destinatePos = target.transform.position;
        }
        
        StartCoroutine(CooldownAttack());
    }



    void Dashing()
    {
        if(dashState)
        {
            dashing = true;
            body.transform.position += new Vector3(body.velocity.x * dashState.dashSpeed, 0);

            StartCoroutine(CooldownDash());
        }
    }



    void GetStatus()
    {
        if (curHealth <= 0)
        {
            body.simulated = false;
            Destroy(gameObject, 1);
        }

        if (curDistance <= triggerDistance)
        {
            ChasingTarget();
        }

        if (curDistance <= attackRange && canAttack)
        {
            canAttack = false;
            AttackingTarget();
        }

        if (curDistance > attackRange && canDash && dashState)
        {
            canDash = false;
            Dashing();
        }
    }

    IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(dashCdTime);
        canDash = true;
    }

    IEnumerator CooldownAttack()
    {
        yield return new WaitForSeconds(attackCdTime);
        canAttack = true;
    }

    void ChoseState()
    {
        if (curHealth <= 0)
        {
            SetState(dieState);
            return;
        }

        if (hurting)
        {
            SetState(hurtState);
            return;
        }

        if (dashing && dashState)
        {
            SetState(dashState);
            return;
        }

        if (norAttacking)
        {
            if(!rangedAttack)
            {
                SetState(normalAttackState);
                return;
            }
            else
            {
                SetState(rangeAttackState);
                return;
            }
        }

        if (charging && chargeAttackState)
        {
            SetState(chargeAttackState);
            return;
        }

        if (body.velocity.magnitude > 0)
        {
            SetState(runState);
            return;
        }

        SetState(idleState);
    }

    public void ChangeHealth(float amount, bool harmed)
    {
        if (amount > 0.1f)
        {
            if (harmed)
            {
                curHealth = Mathf.Clamp(curHealth - amount, 0, maxHealth);
                hurting = true;
            }
            else
            {
                curHealth = Mathf.Clamp(curHealth + amount, 0, maxHealth);
            }
        }
    }
}
