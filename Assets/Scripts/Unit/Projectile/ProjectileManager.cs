using System.Collections;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    private UnitStats ProjectileStats => projectile.stats;
    private UnitBehavior ProjectileBehaviors => projectile.behaviors;
    private AnimState State => projectile.stateMachine.state;

    private Vector2 flyDir;

    public void Init(Transform source, Vector2 _flyDir)
    {
        projectile.SetInstance();

        gameObject.SetActive(true);
        gameObject.transform.SetPositionAndRotation(source.position, source.rotation);
        this.flyDir = _flyDir;

        if (projectile.FlyType != EProjFlyType.Flip)
        {
            RotateProjectile();
        }
        else
        {
            ChangeFaceDir();
        }

        SetState(ProjectileBehaviors.attackState);
    }

    public void SetState(AnimState _state, bool forceChange = false)
    {
        StartCoroutine(projectile.stateMachine.SetState(_state, forceChange));
    }

    private void ChangeFaceDir()
    {
        if (flyDir.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(flyDir.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void RotateProjectile()
    {
        float angle = Mathf.Atan2(flyDir.y, flyDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + angle);
        //transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + angle);
    }

    private void ProjectileFly()
    {
        projectile.core.body.position = Vector2.Lerp(projectile.core.body.position, projectile.core.body.position + flyDir, ProjectileStats.CurSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        State?.UpdateBranchState();

        if (ProjectileBehaviors.attackState.status == EStateStatus.Finish)
        {
            StartCoroutine(ReturnToPool());
        }
    }

    private void FixedUpdate()
    {
        State?.FixUpdateBranchState();

        ProjectileFly();
    }

    private IEnumerator ReturnToPool()
    {
        if (ProjectileBehaviors.dieState)
        {
            SetState(ProjectileBehaviors.dieState);
            yield return new WaitForSeconds(1);

            ProjectileBehaviors.dieState.ExitState();
        }
        else
        {
            ProjectileBehaviors.attackState.ExitState();
            yield return null;
        }

        flyDir = Vector2.zero;
        //transform.localScale = Vector3.one;

        gameObject.SetActive(false);

        ProjectilePooling.Instance.ReturnObjectToPool(projectile);
    }
}
