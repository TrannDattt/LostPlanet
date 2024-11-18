using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Windows;

public abstract class AEnemyController : AController
{
    // State
    public Idle idleState;
    public Run runState;
    public NormalAttack normalAttackState;
    public Hurt hurtState;
    public Die dieState;

    private float curDistance;

    public float triggerRange;
    public float attackRange;

    public override void SetInstance()
    {
        base.SetInstance();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    protected void Update()
    {
        state.UpdateBranchState();

        GetTargetDistance();
        SelectState();
    }

    protected void FixedUpdate()
    {
        state.FixUpdateBranchState();

        UpdateteMoveDir();
        ChangeFaceDir();

        if(gameObject.name == "ToasterBot")
        {
            Debug.Log(state.ToString());
        }
    }

    protected virtual void SelectState()
    {
        if (curHealth <= 0 && !isDeath)
        {
            isDeath = true;
            SetState(dieState);
            TMPPooling.Instance.SpawnCoinTMP(target.transform.position + Vector3.up * 0.5f, "+" + coinHave.ToString());
            StartCoroutine(ReturnToPool());
            return;
        }

        if (hurting || !hurtState.Completed)
        {
            SetState(hurtState);
            return;
        }

        if ((curDistance <= attackRange && canAttack) || (!normalAttackState.Completed))
        {
            canAttack = false;
            SetState(normalAttackState);
            return;
        }

        if (!Mathf.Approximately(MoveDir.magnitude, 0))
        {
            SetState(runState);
            return;
        }

        SetState(idleState);
    }

    // Start is called before the first frame update
    public void Spawn(Vector2 spawnPos)
    {
        gameObject.SetActive(true);

        transform.position = spawnPos;

        SetInstance();
        SetState(idleState);
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);

        EnemyPooling.Instance.ReturnObjectToPool(this);
    }

    private void ChangeFaceDir()
    {
        Vector2 faceDir = (target.transform.position - body.transform.position).normalized;

        if (curDistance > 0.05f)
        {
            body.transform.localScale = new Vector3(Mathf.Sign(faceDir.x), 1, 1);
        }
    }

    private void UpdateteMoveDir()
    {
        if (curDistance > attackRange && curDistance <= triggerRange)
        { 
            MoveDir = (target.transform.position - body.transform.position).normalized; 
        }
        else
        {
            MoveDir = Vector2.zero;
        }
    }

    private void GetTargetDistance()
    {
        curDistance = Vector2.Distance(target.transform.position, gameObject.transform.position);
    }

    public override void ChangeHealth(float amount, bool harmed = true)
    {
        if (!harmed)
        {
            curHealth = Mathf.Clamp(curHealth + amount, 0, maxHealth);
        }
        else
        {
            curHealth = Mathf.Clamp(curHealth - amount, 0, maxHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            SingleAttack attack = collision.GetComponent<SingleAttack>();

            if (canHurt && attack)
            {
                canHurt = false;
                hurting = true;

                ChangeHealth(attack.damage);
                TMPPooling.Instance.SpawnDamageTMP((Vector2)transform.position + Vector2.up * 0.5f, attack.damage.ToString());
                KnockBack(attack.knockBackForce);
            }
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            AProjectile projectile = collision.GetComponent<AProjectile>();

            if (canHurt && projectile)
            {
                canHurt = false;
                hurting = true;

                ChangeHealth(projectile.damage);
                TMPPooling.Instance.SpawnDamageTMP((Vector2)transform.position + Vector2.up * 0.5f, projectile.damage.ToString());
                KnockBack(projectile.knockBackForce);
            }
        }
    }
}
