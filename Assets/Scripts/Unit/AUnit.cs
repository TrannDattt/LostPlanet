using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AUnit : MonoBehaviour
{
    [field: SerializeField] public UnitCore Core {  get; private set; }
    [field: SerializeField] public UnitStatus Status { get; private set; }
    public Vector2 MoveDir { get; protected set; }

    [SerializeField] protected AnimStateMachine animStateMachine;

    public virtual void Init()
    {
        Status.Init(this);
        animStateMachine.Init(this);
    }

    protected virtual void FixedUpdate()
    {
        ChangeFaceDir();
    }

    protected virtual void ChangeFaceDir()
    {
        if(MoveDir.x != 0)
        { 
            transform.localScale = new Vector3(Mathf.Sign(MoveDir.x), 1, 1); 
        }
    }

    protected virtual async void OnDying() => await Task.Delay(1000);

    private bool IsSameFactor(AUnit unitA, AUnit unitB)
    {
        return unitA.GetType().Equals(unitB.GetType());
    }

    public void DoDamageToUnit(AUnit damagedUnit, float amount)
    {
        if(!IsSameFactor(this, damagedUnit))
        {
            damagedUnit.TakeDamage(amount);
        }
    }

    public void TakeDamage(float amount)
    {
        if (animStateMachine.StateDict[AnimStateMachine.EStateKey.Hurt].Status == EStatus.Ready)
        {
            Status.ChangeCurHealth(amount);
            animStateMachine.TransitToState(AnimStateMachine.EStateKey.Hurt);
        }
    }

    //private void OnEnable()
    //{
    //    //OnChangingHealth += TMPPooling.Instance.GetFromPool(ene)
    //}

    //private void OnDisable()
    //{
    //    OnChangingHealth = null;
    //    //Dying = null;
    //}
}

public enum EUnitType
{
    Player,
    Enemy,
}
