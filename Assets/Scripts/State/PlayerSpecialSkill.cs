using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialSkill : State
{
    public HoldAttack holdAttackState;
    public SingleAttack singleAttackState;

    public KeyCode keyHold;
    public float resetAttackTime;

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void FixUpdateState()
    {
        
    }

    public override void UpdateState()
    {
        SelectState();

        if(singleAttackState.Completed && ChildState == singleAttackState)
        {
            StartCoroutine(ResetAttack());
            Completed = true;
        }
    }

    private void SelectState()
    {
        if (Input.GetKeyUp(keyHold)) 
        {
            SetState(singleAttackState);
            return;
        }

        if(Input.GetKey(keyHold) && ChildState != singleAttackState)
        {
            SetState(holdAttackState);
            return;
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(resetAttackTime);
        core.canAttack = true;
    }
}
