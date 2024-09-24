using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoxer : BaseEnemy
{
    // Teleport
    bool canTp = true;
    [SerializeField] float tpDistance;
    [SerializeField] float tpCd;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (enemyController.Target.transform.position - transform.position).normalized;
        float distance = (enemyController.Target.transform.position - transform.position).magnitude;

        if (canTp && enemyController.isChasing && distance > attackRange && !enemyController.isInAnimation)
        {
            canTp = false;
            enemyController.isInAnimation = true;
            StartCoroutine(Teleport(direction));
        }
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

    protected override IEnumerator InitiateAttack()
    {
        AnimationClip attackClip = GetAnimationClip("Attack");
        float clipLength = 1.5f * attackClip.length;

        yield return new WaitForSeconds(clipLength * 10 / 16);
        Vector3 spawnPosition = enemyController.Target.transform.position;

        yield return new WaitForSeconds(clipLength * 4 / 16);
        GameObject firedProjectile = Instantiate(projectile, spawnPosition + Vector3.down * 0.25f, Quaternion.identity);
        firedProjectile.GetComponent<Projectile>().Damage = Damage;

        //Debug.Log(firedProjectile.GetComponent<Projectile>().damage);
        enemyController.isInAnimation = false;
    }

    // Teleport
    IEnumerator Teleport(Vector3 direction)
    {
        StartCoroutine(ResetTp());

        AnimationClip tpClip = GetAnimationClip("Teleport");
        animator.SetTrigger("teleport");

        yield return new WaitForSeconds(tpClip.length * 7 / 10);
        transform.position += tpDistance * direction;

        yield return new WaitForSeconds(tpClip.length * 3 / 10);
        enemyController.isInAnimation = false;
    }

    IEnumerator ResetTp()
    {
        yield return new WaitForSeconds(tpCd);
        canTp = !canTp;
    }
}
