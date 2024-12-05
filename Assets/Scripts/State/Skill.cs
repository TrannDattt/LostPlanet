using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : AnimState
{
    [System.Serializable]
    public class SkillSet
    {
        public AnimationClip holdClip;
        public AnimationClip releaseClip;
        //public Projectile? projectile;
    }

    [SerializeField] private List<SkillSet> skillSets;

    [SerializeField] private HoldAttack holdState;
    [SerializeField] private ReleaseSkill releaseState;
    //public Projectile? projectile;
    public KeyCode keyHold;

    [SerializeField] private EUnitType doDamageToUnit;
    public float DamageDo {  get; private set; }

    private void Start()
    {
        InitialChildStates(holdState, releaseState);
    }

    public override void EnterState()
    {
        base.EnterState();

        SelectSkill();
        SetState(holdState);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        SelectState();
    }

    public override void FixUpdateState()
    {
        base.FixUpdateState();

        Body.velocity = Vector3.zero;
    }

    public override void ExitState()
    {
        releaseState.ExitState();

        base.ExitState();
    }

    public void SelectSkill()
    {
        SetSkill(skillSets[0]);
    }

    public void SetSkill(SkillSet skillSet)
    {
        holdState.clip = skillSet.holdClip;
        releaseState.clip = skillSet.releaseClip;
        DamageDo = Unit.stats.CurDamage * releaseState.damageMultiplier;
        // projectile
    }

    private void SelectState()
    {
        if ((Input.GetKeyUp(keyHold)) || keyHold == KeyCode.None || holdState == null)
        {
            SetState(releaseState);
            return;
        }
    }

    protected override void CheckFinishState()
    {
        if (releaseState.status == EStateStatus.Finish)
        {
            status = EStateStatus.Finish;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision && collision.gameObject.TryGetComponent<AUnit>(out AUnit collidedUnit))
    //    {
    //        if (collidedUnit.unitType == EUnitType.Enemy && collidedUnit.unitType == doDamageToUnit)
    //        {
    //            collidedUnit.gameObject.GetComponent<EnemyManager>()?.ChangeHealth(collidedUnit.stats.CurDamage * releaseState.damageMultiplier);
    //        }
    //        else if (collidedUnit.unitType == EUnitType.Player && collidedUnit.unitType == doDamageToUnit)
    //        {
    //            PlayerManager.Instance.OnDecreaseHealth(collidedUnit.stats.CurDamage * releaseState.damageMultiplier);
    //        }
    //    }
    //}
}
