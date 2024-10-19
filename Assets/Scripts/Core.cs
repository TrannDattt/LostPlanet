using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Core : MonoBehaviour
{
    public float maxHealth;
    public float curHealth { get; protected set; }

    public float damage;

    public int coinHave;

    protected StateMachine stateMachine;

    public Rigidbody2D body;
    public AColliderDectector colliderDitector;
    public Animator animator;

    public State state => stateMachine.state;

    //public bool rangedAttack = false;

    public bool norAttacking { get; set; }
    public bool charging { get; set; }
    public bool heavyAttacking { get; set; }
    public bool dashing { get; set; }
    public bool hurting { get; set; }
    public bool death {  get; set; }

    public void SetInstance()
    {
        curHealth = maxHealth;
        norAttacking = false;
        charging = false;
        heavyAttacking = false;
        dashing = false;
        hurting = false;

        stateMachine = new StateMachine();
        State[] allState = GetComponentsInChildren<State>();

        foreach(State state in allState)
        {
            state.SetCore(this);
        }
    }

    protected virtual void Update()
    {
        state.UpdateBranchState();
    }

    protected virtual void FixedUpdate()
    {
        state.FixUpdateBranchState();
    }

    protected void SetState(State _state, bool forceReset = false)
    {
        stateMachine.SetState(_state, forceReset);
    }

    private void OnDrawGizmos()
    {
        
    }
}
