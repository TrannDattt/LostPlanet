using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    // Controller
    protected EnemyController enemyController;

    // Attack
    public bool canAttack = true;
    [SerializeField] int baseDamage;
    public int Damage { get { return baseDamage; } protected set { baseDamage = value; } }
    [SerializeField] float attackCd;
    public float attackRange;

    public GameObject projectile;
    public GameObject meleeAttackHitbox;

    // Animation
    protected Animator animator;
    RuntimeAnimatorController runtimeAC;
    protected Dictionary<string, AnimationClip> clipDict;

    protected Coroutine currentCoroutine;

    public virtual void Start()
    {
        enemyController = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();

        clipDict = new Dictionary<string, AnimationClip>();
        runtimeAC = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in runtimeAC.animationClips)
        {
            clipDict[clip.name] = clip;
        }

        if (meleeAttackHitbox)
        {
            meleeAttackHitbox.SetActive(false);
        }
    }

    protected AnimationClip GetAnimationClip(string name)
    {
        if (clipDict.TryGetValue(name, out AnimationClip clip))
        {
            return clip;
        }
        return null;
    }

    public virtual IEnumerator Attack()
    {
        yield return null;
    }

    protected virtual IEnumerator InitiateAttack()
    {
        yield return null;
    }

    protected IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCd);
        canAttack = true;
    }
}
