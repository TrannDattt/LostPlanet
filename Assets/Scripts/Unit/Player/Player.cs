using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AUnit
{
    public static Player Instance {  get; private set; }

    [SerializeField] private InputAction moveAction;

    private bool isDied;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        { 
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        //Init();
        gameObject.SetActive(false);
    }

    public override void Init()
    {
        base.Init();

        isDied = false;
        moveAction.Enable();
    }

    private void Update()
    {
        GetMovementInput();

        if(Status.CurHealth <= 0 && !isDied)
        {
            isDied = true;
            PlayerDie();
        }
    }

    private void GetMovementInput()
    {
        MoveDir = moveAction.ReadValue<Vector2>();
    }

    private async void PlayerDie()
    {
        Core.body.simulated = false;
        await Task.Delay(1000);
        GameManager.Instance.LevelFailed();
    }
}
