using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : AUnit
{
    public AnimState idleState;

    public override void SetInstance()
    {
        base.SetInstance();

        //target = GameObject.Find("Player");
    }

    private void Start()
    {
        SetInstance();

        //SetState(idleState);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
