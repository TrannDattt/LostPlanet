using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AProjectile : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;

    public float flySpeed;
    public float damage;
    public float knockBackForce = 1;

    public float activeTime;
    private float timeCount;

    public bool canRotate = true;

    protected Vector2 flyDir;
    protected bool isShooting;

    public virtual void SetInstance(Transform source, Vector2 targetPos)
    {
        gameObject.transform.position = source.position;
        gameObject.transform.rotation = source.rotation;
        gameObject.SetActive(true);

        flyDir = (targetPos - (Vector2)transform.position).normalized;
        isShooting = true;

        if(canRotate)
        { 
            RotateProjectile(); 
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Sign(flyDir.x), 1, 1);
        }
    }

    public virtual void Fly()
    {

    }

    private void ReturnToPool()
    {
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);

        flyDir = Vector2.zero;
        isShooting = false;
        timeCount = 0;

        transform.localScale = Vector3.one;

        ProjectilePooling.Instance.ReturnObjectToPool(this);
    }

    public virtual void FixedUpdate()
    {
        if (isShooting)
        {
            timeCount += Time.fixedDeltaTime;
            body.MovePosition(body.position + flySpeed * Time.fixedDeltaTime * flyDir);
        }

        if(timeCount > activeTime)
        {
            ReturnToPool();
        }
    }

    private void RotateProjectile()
    {
        float angle = Mathf.Atan2(flyDir.y, flyDir.x) * Mathf.Rad2Deg;

        body.transform.localRotation = Quaternion.Euler(body.transform.localRotation.x, body.transform.localRotation.y, body.transform.localRotation.z + angle);
    }
}
