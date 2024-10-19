using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AProjectile : Core
{
    public State initState;

    public bool isRotate;
    public float flyDistance;

    public Vector3 spawnPos {  get; set; }
    public Vector3 destinatePos { get; set; }

    protected virtual void Awake()
    {
        SetInstance();

        SetState(initState);
        spawnPos = transform.position;
    }

    protected virtual void Start()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) 
    //    {
    //        //Destroy(gameObject);
    //        PlayerController player = collision.collider.gameObject.GetComponent<PlayerController>();
    //        player.ChangeHealth(damage, true);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
