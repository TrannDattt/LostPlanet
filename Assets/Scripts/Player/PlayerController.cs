﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerController : Core
{
    // State
    public Idle idleState;
    public Run runState;
    public Dash dashState;
    public NormalAttack normalAttackState;
    public ChargeAttack chargeAttackState;
    public ReleaseChargeAttack releaseChargeAttackState;
    public Hurt hurtState;
    public Die dieState;

    public bool rangedAttack = false;

    //For ranged attack
    public GameObject projectileObject;
    protected GameObject spawnProjectile;

    float holdTimeCount;

    public InputAction moveAction;

    public float inputX { get; private set; }
    public float inputY { get; private set; }

    // UI
    //[SerializeField] GameObject charUI;

    // Start is called before the first frame update
    void Start()
    {
        moveAction.Enable();
        moveAction.performed += Move;

        SetInstance();

        SetState(idleState);
    }

    private void Move(InputAction.CallbackContext context)
    {
        
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

        SetBodyVelocity();
    }

    void GetStatus()
    {
        if(curHealth <= 0)
        {
            body.simulated = false;
            //Destroy(gameObject, 1);
        }

        Vector2 direction = moveAction.ReadValue<Vector2>();
        inputX = direction.x;
        inputY = direction.y;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            norAttacking = true;
            //holdTimeCount = Time.time;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time - holdTimeCount > chargeAttackState.chargeTime)
            {
                charging = true;
            }
        }
        else
        {
            holdTimeCount = Time.time;
        }

        if (charging)
        {
            heavyAttacking = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            dashing = true;
            Dashing();
        }
    }

    void SetBodyVelocity()
    {
        if (norAttacking || charging)
        {
            body.velocity = Vector2.zero;
            return;
        }

        if(inputX != 0)
        {
            body.transform.localScale = new Vector3(Mathf.Sign(inputX), 1, 1);
        }

        body.velocity = new Vector2(inputX, inputY) * runState.runSpeed;
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

        if (dashing)
        {
            SetState(dashState);
            return;
        }

        if (heavyAttacking && !charging)
        {
            SetState(releaseChargeAttackState);
            return;
        }

        if (norAttacking)
        {
            if (!rangedAttack)
            {
                SetState(normalAttackState);
                return;
            }
            
        }

        if (charging)
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
        if(amount > 0.1f)
        {
            Debug.Log(amount);
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

    void Dashing()
    {
        body.transform.position += new Vector3(body.velocity.x * dashState.dashSpeed, 0);
    }









    //public void GainCoin(int value)
    //{
    //    coinHave += value;
    //    coinText.text = coinHave.ToString();
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject collidedObject = collision.gameObject;

    //    if (collidedObject.layer == LayerMask.NameToLayer("EnemyMeleeAttackHitbox"))
    //    {
    //        EnemyController enemy = collidedObject.GetComponentInParent<EnemyController>();
    //        ChangeHealth(enemy.Damage);
    //        StartCoroutine(GetHit());
    //    }

    //    if (collidedObject.layer == LayerMask.NameToLayer("EnemyRangeAttackHitbox"))
    //    {
    //        Projectile enemyProjectile = collidedObject.GetComponent<Projectile>();
    //        ChangeHealth(enemyProjectile.Damage);
    //        StartCoroutine(GetHit());
    //    }
    //}
}
