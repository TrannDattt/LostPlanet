using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BnCBot : BaseEnemy
{
    bool canLeap = true;
    [SerializeField] float leapCd = 2f;

    bool isChangedPhase = false;

    // Update is called once per frame
    void Update()
    {
        if(enemyController.HealthPercentage <= 0.4 && !isChangedPhase)
        {
            isChangedPhase = true;
            enemyController.isInAnimation = true;
            enemyController.StopActiveCoroutines();
            StartCoroutine(ChangePhase());
        }

        if ((enemyController.Target.transform.position - transform.position).magnitude > attackRange && enemyController.isChasing 
            && !enemyController.isInAnimation && isChangedPhase && canLeap)
        {
            canLeap = false;
            enemyController.isInAnimation = true;
            currentCoroutine = StartCoroutine(Leap());
            enemyController.AddCorountine(currentCoroutine);
        }
    }

    public override IEnumerator Attack()
    {
        StartCoroutine(ResetAttack());

        yield return new WaitForSeconds(1);
        animator.SetTrigger("attack");
        currentCoroutine = StartCoroutine(InitiateAttack());
        enemyController.AddCorountine(currentCoroutine);
    }

    protected override IEnumerator InitiateAttack()
    {
        if (meleeAttackHitbox)
        {
            AnimationClip attackClip = GetAnimationClip("Attack");
            float clipLength = attackClip.length;

            meleeAttackHitbox.SetActive(true);

            yield return new WaitForSeconds(clipLength * 2 / 8);
            meleeAttackHitbox.SetActive(false);


            yield return new WaitForSeconds(clipLength * 2 / 8);
            meleeAttackHitbox.SetActive(true);


            yield return new WaitForSeconds(clipLength * 2 / 8);
            meleeAttackHitbox.SetActive(false);

            yield return new WaitForSeconds(clipLength * 2 / 8);
            enemyController.isInAnimation = false;
        }
    }

    IEnumerator ChangePhase()
    {
        AnimationClip chargeClip = GetAnimationClip("Charge");
        float clipLength = chargeClip.length * 4;

        animator.SetTrigger("changedPhase");
        Damage = Damage * 4 / 3;
        enemyController.ChangeHealth(-0.2f);

        yield return new WaitForSeconds(clipLength);
        enemyController.isInAnimation = false;
    }

    IEnumerator Leap()
    {
        AnimationClip leapClip = GetAnimationClip("Leap");
        float clipLength = leapClip.length;

        animator.SetTrigger("leap");
        StartCoroutine(ResetLeap());

        yield return new WaitForSeconds(clipLength * 1 / 5);
        float elapsedTime = 0f;
        Vector3 leapLocation = enemyController.Target.transform.position;

        while (elapsedTime < clipLength * 3 / 5)
        {
            transform.position = Vector3.Lerp(transform.position, leapLocation, (elapsedTime / (clipLength * 3 / 5)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemyController.isInAnimation = false;
    }

    IEnumerator ResetLeap()
    {
        yield return new WaitForSeconds(leapCd);
        canLeap = true;
    }
}
