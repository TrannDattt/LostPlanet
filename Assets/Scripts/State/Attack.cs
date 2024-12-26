using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static AnimStateMachine;

[System.Serializable]
public class Attack : AAnimState<EStateKey>
{
    [SerializeField] private List<AttackSet> attackSets;
    //[SerializeField] private GameObject hitbox;
    public float DamageMulti {  get; private set; }
    private int attackIndex;
    private bool canChangeAttack;

    //public Attack(EStateKey stateKey, AUnit unit) : base(stateKey, unit) { }

    public override void EnterState()
    {
        SetAttack(attackIndex);
        unit.Core.animator.speed = playSpeed;
        unit.Core.animator.Play(clip.name);
        startTime = Time.time;
        Status = EStatus.Doing;
        canChangeAttack = false;

        if (attackSets[attackIndex].projectile != null)
        {
            var projectile = ProjectilePooling.Instance.GetFromPool(attackSets[attackIndex].projectile, unit.transform.position);
            projectile.OnDoingDamage += DoDamage;
        }
    }

    public override EStateKey GetNextState()
    {
        if (Status == EStatus.Finished)
        {
            return EStateKey.Idle; 
        }

        if (unit is Player && Input.GetKeyDown(KeyCode.Mouse0) && canChangeAttack)
        {
            SelectNextAttackSkill();
            EnterState();
        }

        return EStateKey.Attack;
    }

    public override void UpdateState() 
    {
        if (PlayedTime > clip.length / unit.Core.animator.speed)
        {
            canChangeAttack = true;

            if (unit is not Player)
            {
                Status = EStatus.Finished;
            }
        }

        if (unit is Player && PlayedTime > .2f + clip.length / unit.Core.animator.speed)
        {
            Status = EStatus.Finished;
        }
    }

    public override void FixUpdateState() { }

    public override void ExitState() => attackIndex = 0;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<AUnit>(out AUnit damagedUnit))
        {
            DoDamage(damagedUnit);
        }
    }

    public override void OnTriggerStay2D(Collider2D other) { }

    public override void OnTriggerExit2D(Collider2D other) { }

    private void SelectNextAttackSkill() => attackIndex = (attackIndex + 1) % attackSets.Count;

    private void SetAttack(int index)
    {
        clip = attackSets[index].clip;
        DamageMulti = attackSets[attackIndex].damageMulti;
    }

    public void DoDamage(AUnit damagedUnit) => unit.DoDamageToUnit(damagedUnit, -1 * unit.Status.CurDamage * DamageMulti);
}
