using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AnimState
{
    [System.Serializable]
    public class AttackMove
    {
        public float damageMultiplier;
        public AnimationClip clip;
        public Projectile projectile;
    }

    [SerializeField] private List<AttackMove> attackMoves;
    private Queue<AttackMove> attackMoveQueue = new();

    public float DamageDo { get; private set; }
    [SerializeField] private EUnitType doDamageToUnit;
    private Projectile projectile;

    private void SelectAttackMove()
    {
        var attackMove = attackMoveQueue.Dequeue();
        
        clip = attackMove.clip;
        DamageDo = attackMove.damageMultiplier * Unit.stats.CurDamage;
        projectile = attackMove.projectile;

        attackMoveQueue.Enqueue(attackMove);
    }

    public override void EnterState()
    {
        SelectAttackMove();

        base.EnterState();

        if (projectile != null)
        {
            if (Unit is Enemy _enemy)
            {
                ProjectilePooling.Instance.FireProjectile(projectile, _enemy.transform, _enemy.Target.transform.position - _enemy.transform.position);
            }
            else if (Unit is Player _player)
            {
                //Get mouse pos
            }
        }
    }

    public override void FixUpdateState()
    {
        base.FixUpdateState();

        Body.velocity = Vector3.zero;
    }

    private void Start()
    {
        foreach (var attackMove in attackMoves)
        {
            attackMoveQueue.Enqueue(attackMove);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision && collision.gameObject.TryGetComponent<AUnit>(out AUnit collidedUnit))
    //    {
    //        if (collidedUnit.unitType == EUnitType.Enemy && collidedUnit.unitType == doDamageToUnit)
    //        {
    //            collidedUnit.gameObject.GetComponent<EnemyManager>()?.ChangeHealth(collidedUnit.stats.CurDamage * DamageDo);
    //            Debug.Log("do damage");
    //        }
    //        else if (collidedUnit.unitType == EUnitType.Player && collidedUnit.unitType == doDamageToUnit)
    //        {
    //            PlayerManager.Instance.OnDecreaseHealth(collidedUnit.stats.CurDamage * DamageDo);
    //            Debug.Log("do damage 2");
    //        }
    //        //if (collidedUnit.unitType == doDamageToUnit)
    //        //{
    //        //    collidedUnit.DecreaseHealth(collidedUnit.stats.CurDamage * damageMultiplier);
    //        //}
    //    }
    //}
}
