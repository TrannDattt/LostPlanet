using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerController : AController
{
    // State
    public Idle idleState;
    public Run runState;
    public Dash dashState;
    public NormalAttack normalAttackState;
    public PlayerSpecialSkill specialSkillState;
    public Hurt hurtState;
    public Die dieState;

    public InputAction moveAction;

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
    protected void Update()
    {
        state.UpdateBranchState();

        SelectState();
    }

    protected void FixedUpdate()
    {
        state.FixUpdateBranchState();

        GetMovementInput();
        ChangeFaceDir();
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1);
        ServiceLocator.Instance.PlayerDie();
    }

    private void GetMovementInput()
    {
        MoveDir = moveAction.ReadValue<Vector2>();
    }

    private void ChangeFaceDir()
    {
        if (MoveDir.x != 0)
        {
            body.transform.localScale = new Vector3(Mathf.Sign(MoveDir.x), 1, 1);
        }
    }

    private void SelectState()
    {
        if(curHealth <= 0 && !isDeath)
        {
            isDeath = true;
            StartCoroutine(StopGame());
            SetState(dieState);
            return;
        }

        if ((Input.GetKey(specialSkillState.keyHold) && canAttack) || (!specialSkillState.Completed))
        {
            canAttack = false;
            SetState(specialSkillState);
            return;
        }

        if (hurting || !hurtState.Completed)
        {
            SetState(hurtState);
            return;
        }

        if ((Input.GetMouseButtonDown(1) && canDash) || (!dashState.Completed))
        {
            canDash = false;
            SetState(dashState);
            return;
        }

        if ((Input.GetMouseButtonUp(0) && canAttack) || (!normalAttackState.Completed))
        {
            canAttack = false;
            SetState(normalAttackState);
            return;
        }

        if (!Mathf.Approximately(MoveDir.magnitude, 0) && dashState.Completed)
        {
            SetState(runState);
            return;
        }

        SetState(idleState);
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

    public void ChangeCoinHave(int amount, bool isGain)
    {
        if(isGain)
        { 
            coinHave += amount; 
        }
        else
        {
            coinHave -= amount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            SingleAttack attack = collision.GetComponent<SingleAttack>();

            if (canHurt && attack)
            {
                canHurt = false;
                hurting = true;
                ChangeHealth(attack.damage);
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            AProjectile projectile = collision.GetComponent<AProjectile>();

            if (canHurt && projectile)
            {
                canHurt = false;
                hurting = true;
                ChangeHealth(projectile.damage);
            }
        }
    }
}
