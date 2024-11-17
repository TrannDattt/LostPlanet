using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : State
{
    public List<SingleAttack> singleAtttackStates;

    public float resetAttackTime;

    private int attackIndex = 0;
    private int lastAttackIndex;

    public override void EnterState()
    {
        SetState(singleAtttackStates[attackIndex]);

        SelectNextAttack();
    }

    public override void ExitState()
    {

    }

    public override void FixUpdateState()
    {

    }

    public override void UpdateState()
    {
        if (singleAtttackStates[lastAttackIndex].Completed)
        {
            StartCoroutine(ResetAttack());
            Completed = true;
        }
    }

    private void SelectNextAttack()
    {
        lastAttackIndex = attackIndex;
        attackIndex++;

        if(attackIndex >= singleAtttackStates.Count)
        {
            attackIndex = 0;
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(resetAttackTime);
        core.canAttack = true;
    }
}
