using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class AController : MonoBehaviour
{
    public float maxHealth;
    public float curHealth { get; protected set; }

    public int coinHave;

    protected StateMachine stateMachine;

    public Rigidbody2D body;
    //public AColliderDectector colliderDitector;
    public Animator animator;
    public CharacterAudio characterAudio;
    public GameObject target;

    public State state => stateMachine.state;

    //public bool rangedAttack = false;
    public Vector2 MoveDir { get; protected set; }

    public bool canAttack;

    public bool canDash;

    public bool canHurt;
    public bool hurting;

    public virtual void SetInstance()
    {
        curHealth = maxHealth;
        canAttack = true;
        canDash = true;
        canHurt = true;
        hurting = false;

        stateMachine = new StateMachine();
        State[] allState = GetComponentsInChildren<State>();

        foreach(State state in allState)
        {
            state.SetCore(this);
        }
    }

    protected void SetState(State _state, bool forceReset = false)
    {
        stateMachine.SetState(_state, forceReset);
    }

    public virtual void ChangeHealth(float amount, bool harmed = true)
    {

    }

    protected void KnockBack(float knockBackForce)
    {
        if (this is AEnemyController)
        {
            Vector2 knockDir = transform.position - target.transform.position;

            body.AddForce(knockBackForce * knockDir); 
            //Debug.Log(knockBackForce * MoveDir);
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
