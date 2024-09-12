using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    // Movement
    Rigidbody2D enemyRb;
    [SerializeField] float baseSpeed;
    float curSpeed;
    int lookDirection = 1;

    // Health
    [SerializeField] int baseHealth;
    int curHealth;

    // Attack
    bool canAttack = true;
    public int baseDamage;
    [SerializeField] float attackCd;
    [SerializeField] float attackRange;
    public GameObject attackHitbox;
    int attackType = 0;
    int attackTypeCount = 2;

    // Die
    bool isDeath = false;

    // Animation
    Animator animator;
    RuntimeAnimatorController runtimeAC;
    Dictionary<string, AnimationClip> clipDict;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        clipDict = new Dictionary<string, AnimationClip>();
        runtimeAC = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in runtimeAC.animationClips)
        {
            clipDict[clip.name] = clip;
        }

        curHealth = baseHealth;
        attackHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Die
        if (curHealth <= 0 && !isDeath)
        {
            isDeath = true;
            animator.SetTrigger("die");
            Destroy(gameObject, 1);
            enemyRb.simulated = false;
        }

        // Attack
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * lookDirection, attackRange, LayerMask.GetMask("Player"));
        if (hit && canAttack)
        {
            Attack();
            canAttack = false;
        }
    }

    AnimationClip GetAnimationClip(string name)
    {
        if(clipDict.TryGetValue(name, out AnimationClip clip))
        {
            return clip;
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.layer == LayerMask.NameToLayer("PlayerAttackHitbox"))
        {
            PlayerController player = collidedObject.GetComponentInParent<PlayerController>();
            ChangeHealth(player.baseDamage);
        }
    }

    void ChangeHealth(int amount)
    {
        curHealth -= amount;
    }

    void Attack()
    {
        CancelInvoke("ResetAttackType");
        //animator.SetFloat("attackType", attackType);
        attackType = attackType >= attackTypeCount ? 0 : attackType + 1;

        animator.SetTrigger("attack");
        StartCoroutine("ChangeAttackHitboxState");

        Invoke("ResetAttack", attackCd);
        Invoke("ResetAttackType", 2);
    }

    IEnumerator ChangeAttackHitboxState()
    {
        AnimationClip attackClip = GetAnimationClip("Attack");
        float clipLength = attackClip.length;

        yield return new WaitForSeconds(clipLength * 13 / 17);
        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(clipLength * 4 / 17);
        attackHitbox.SetActive(false);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    void ResetAttackType()
    {
        attackType = 0;
    }
}
