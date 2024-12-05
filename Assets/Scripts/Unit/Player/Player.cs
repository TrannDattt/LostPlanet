using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AUnit
{
    public InputAction moveAction;

    public delegate void ChangeHealth(float amount);
    public event ChangeHealth DecreaseHealth;
    public event ChangeHealth IncreaseHealth;

    //public delegate void ChangeCoin();
    //public static event ChangeCoin DecreaseCoin;
    //public static event ChangeCoin IncreaseCoin;

    public override void SetInstance()
    {
        base.SetInstance();

        moveAction.Enable();
    }

    protected void FixedUpdate()
    {
        GetMovementInput();
        //CheckEventHappen();
    }

    //private void CheckEventHappen()
    //{
    //    DecreaseCoin?.Invoke();
    //    IncreaseCoin?.Invoke();
    //}

    private void GetMovementInput()
    {
        MoveDir = moveAction.ReadValue<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent<Attack>(out Attack attack))
            {
                if (attack.gameObject.CompareTag("EnemyAttack"))
                {
                    DecreaseHealth(attack.DamageDo);
                }
            }
            else if (collision.gameObject.TryGetComponent<Skill>(out Skill skill))
            {
                if (skill.gameObject.CompareTag("EnemyAttack"))
                {
                    DecreaseHealth(skill.DamageDo);
                }
            }
        }
    }
}
