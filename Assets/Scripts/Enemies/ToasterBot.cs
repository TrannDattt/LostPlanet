using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterBot : BaseEnemy
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator Attack()
    {
        StartCoroutine(ResetAttack());

        yield return new WaitForSeconds(1);

        animator.SetTrigger("attack");
        currentCoroutine = StartCoroutine(InitiateAttack());
        enemyController.AddCorountine(currentCoroutine);

        //Invoke("ResetAttackType", 2);
    }

    float GetRotation()
    {
        Vector2 position = enemyController.Target.transform.position - transform.position;

        float angleRad = Mathf.Atan2(position.y, position.x);
        float angleDeg = Mathf.Rad2Deg * angleRad;

        return angleDeg;
    }

    protected override IEnumerator InitiateAttack()
    {
        AnimationClip attackClip = GetAnimationClip("Attack");
        float clipLength = 2 * attackClip.length;

        Vector3 firedPosition = new(transform.position.x + 0.25f * transform.localScale.x, transform.position.y + 0.25f, transform.position.z);

        Quaternion rotation = Quaternion.Euler(0, 0, GetRotation() - 7 * transform.localScale.x);
        GameObject firedProjectile = Instantiate(projectile, firedPosition, rotation);
        firedProjectile.GetComponent<Projectile>().Damage = Damage;

        yield return new WaitForSeconds(clipLength * 3 / 11);
        rotation = Quaternion.Euler(0, 0, GetRotation() - 7 * transform.localScale.x);
        firedProjectile.transform.rotation = rotation;

        //Debug.Log(firedProjectile.GetComponent<Projectile>().damage);
        enemyController.isInAnimation = false;
    }
}
