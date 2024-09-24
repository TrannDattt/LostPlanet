using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [SerializeField] Slider healthBar;
    public int Health { get { return curHealth; } }
    public float HealthPercentage {  get { return (float)curHealth / baseHealth; } }

    // Behavior
    public bool isChasing = false;
    GameObject target;
    public GameObject Target {  get { return target; } }
    Coroutine currentCoroutine;
    public List<Coroutine> ActiveCoroutines { get; private set; }

    // Reward
    [SerializeField] int coinValue = 100;

    // Die
    bool isDeath = false;

    // Animation
    public bool isInAnimation = false;
    Animator animator;
    RuntimeAnimatorController runtimeAC;
    Dictionary<string, AnimationClip> clipDict;

    // Enemy type
    BaseEnemy baseEnemy;
    public int Damage { get { return baseEnemy.Damage; } }

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseEnemy = GetComponent<BaseEnemy>();

        clipDict = new Dictionary<string, AnimationClip>();
        runtimeAC = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in runtimeAC.animationClips)
        {
            clipDict[clip.name] = clip;
        }

        target = GameObject.Find("Player");
        curHealth = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Die
        if (curHealth <= 0 && !isDeath)
        {
            PlayerController player = target.GetComponent<PlayerController>();
            player.GainCoin(coinValue);

            isDeath = true;
            animator.SetTrigger("die");
            Destroy(gameObject, 1);
            enemyRb.simulated = false;
        }


        // Moving + Change direction
        transform.localScale = new Vector2(lookDirection, 1);
        curSpeed = (isChasing && !isInAnimation && !isDeath && !IsInAttackRange()) ? baseSpeed : 0;
        if(animator.HasState(0, Animator.StringToHash("Run")))
        {
            animator.SetFloat("speed", curSpeed);
        }


        // Attack
        float distance = target ? (target.transform.position - transform.position).magnitude : float.PositiveInfinity;
        

        if (baseEnemy.canAttack && IsInAttackRange() && !isInAnimation)
        {
            currentCoroutine = StartCoroutine(baseEnemy.Attack());
            AddCorountine(currentCoroutine);

            isInAnimation = true;
            baseEnemy.canAttack = false;
        }

        // Chasing
        if (distance <= baseEnemy.attackRange)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            Chase();
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

    public void AddCorountine(Coroutine coroutine)
    { 
        ActiveCoroutines.Add(coroutine); 
    }

    public void StopActiveCoroutines()
    {
        foreach(Coroutine coroutine in ActiveCoroutines)
        {
            StopCoroutine(coroutine);
        }
        ActiveCoroutines.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.layer == LayerMask.NameToLayer("PlayerMeleeAttackHitbox"))
        {
            PlayerController player = collidedObject.GetComponentInParent<PlayerController>();
            ChangeHealth(player.baseDamage);
            StartCoroutine(GetHit());
        }
    }

    bool IsInAttackRange()
    {
        float distance = target ? (target.transform.position - transform.position).magnitude : float.PositiveInfinity;
        if(distance <= baseEnemy.attackRange)
        {
            if (baseEnemy.meleeAttackHitbox && Mathf.Abs(Target.transform.position.y - transform.position.y) <= 0.1f)
            {
                return true;
            }

            if (!baseEnemy.meleeAttackHitbox)
            {
                return true;
            }
        }
            
        return false;
    }

    public void ChangeHealth(int amount)
    {
        curHealth = Mathf.Clamp(curHealth - amount, 0, baseHealth);
        healthBar.value = HealthPercentage;
    }

    public void ChangeHealth(float amountPercentage)
    {
        curHealth = (int)Mathf.Clamp(curHealth - amountPercentage * baseHealth, 0, baseHealth);
        healthBar.value = HealthPercentage;
    }

    IEnumerator GetHit()
    {
        AnimationClip getHitClip = GetAnimationClip("GetHit");
        StopActiveCoroutines();
        animator.SetTrigger("getHit");

        if (baseEnemy.meleeAttackHitbox)
        {
            baseEnemy.meleeAttackHitbox.SetActive(false);
        }

        yield return new WaitForSeconds(getHitClip.length);
        isInAnimation = false;
    }

    private void Chase()
    {
        Vector2 direction = target ? target.transform.position - transform.position : Vector2.zero;
        lookDirection = (int)direction.normalized.x > 0 ? 1 : -1;

        if (!IsInAttackRange())
        {
            transform.Translate(curSpeed * Time.deltaTime * direction.normalized);
        }
    }
}
