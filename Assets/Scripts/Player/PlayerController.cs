using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Border
    Border border;

    // Movement
    Rigidbody2D playerRb;
    [SerializeField] float baseSpeed = 2f;
    float curSpeed;
    int lookDirection = 1;

    // Health
    [SerializeField] int baseHealth = 100;
    int curHealth;
    Slider healthBar;

    // Attack
    public int baseDamage = 10;
    [SerializeField] float attackRange = 5f; // For range attack
    [SerializeField] GameObject attackHitbox;
    int attackType = 0;
    int attackTypeCount = 2;

    // Hurt
    bool isInvi = false;
    [SerializeField] float inviTime = 1f;

    // Die
    bool isDeath = false;

    // Action
    public InputAction moveAction;
    public InputAction dashAction;
    public InputAction attackAction;

    // Money
    int coinHave = 0;
    TextMeshProUGUI coinText;

    // Animation
    bool isInAnimation = false;
    Animator animator;
    RuntimeAnimatorController runtimeAC;
    Dictionary<string, AnimationClip> clipDict;

    // UI
    [SerializeField] GameObject charUI;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        border = GameObject.Find("Path").GetComponent<Border>();


        Transform coinTextTransform = charUI.transform.Find("CoinBar/CoinBarContainer/Coin");
        if (coinTextTransform)
        {
            coinText = coinTextTransform.GetComponent<TextMeshProUGUI>();
        }

        healthBar = charUI.transform.Find("HealthBar").GetComponent<Slider>();


        moveAction.Enable();

        dashAction.Enable();
        dashAction.performed += Dash;

        attackAction.Enable();
        attackAction.performed += Attack;


        clipDict = new Dictionary<string, AnimationClip>();
        runtimeAC = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in runtimeAC.animationClips)
        {
            clipDict[clip.name] = clip;
        }


        attackHitbox.SetActive(false);
        curHealth = baseHealth;
        curSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vectorLookDirection = moveAction.ReadValue<Vector2>();


        // Health
        if (curHealth <= 0 && !isDeath)
        {
            isDeath = true;
            StartCoroutine(Die());
        }


        // Change direction
        if(!Mathf.Approximately(vectorLookDirection.x, 0))
        {
            lookDirection = vectorLookDirection.x > 0 ? 1 : -1;
        }
        transform.localScale = new Vector2(lookDirection, 1);
        
        
        // Movement
        //transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3)vectorLookDirection, curSpeed * Time.deltaTime);
        playerRb.position += curSpeed * Time.deltaTime * vectorLookDirection;
        //playerRb.position = MoveTo(vectorLookDirection, curSpeed);
        animator.SetFloat("speed", curSpeed * vectorLookDirection.magnitude);
    }

    private void FixedUpdate()
    {
        playerRb.position = border.Re_positioning(playerRb.position);
    }

    AnimationClip GetAnimationClip(string name)
    {
        if (clipDict.TryGetValue(name, out AnimationClip clip))
        {
            return clip;
        }
        return null;
    }

    void ResetInvoke(String funcName, float timeDelay)
    {
        CancelInvoke(funcName);
        Invoke(funcName, timeDelay);
    }

    IEnumerator Die()
    {
        AnimationClip dieClip = GetAnimationClip("Die");
        animator.SetTrigger("die");
        playerRb.simulated = false;

        yield return new WaitForSeconds(dieClip.length);
        Destroy(gameObject);
        Time.timeScale = 0;
    }


    // Dash
    private void Dash(InputAction.CallbackContext context)
    {
        AnimationClip dashClip = GetAnimationClip("Dash");
        Vector2 vectorLookDirection = moveAction.ReadValue<Vector2>();

        if (!isInAnimation)
        {
            animator.SetTrigger("dash");
            isInAnimation = true;
    
            Vector3 dashDirection = vectorLookDirection != Vector2.zero ? (Vector3)vectorLookDirection : new Vector3(lookDirection, 0);

            StartCoroutine(DashMovement(dashDirection, dashClip.length));
        }
    }

    private IEnumerator DashMovement(Vector3 direction, float duration)
    {
        Vector3 startPosition = transform.position;
        Vector3 destinedPosition = border.Re_positioning(startPosition + direction * duration);
        float elapsedTime = 0f;
        StartCoroutine(IFrame(duration));

        while (2 * elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, destinedPosition, (2 * elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ResetAnimation();
    }

    private IEnumerator IFrame(float duration)
    {
        if(!isInvi)
        {
            isInvi = true;

            yield return new WaitForSeconds(duration);
            isInvi = false;
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        AnimationClip attackClip = GetAnimationClip("Attack" + (attackType + 1).ToString());

        if (!isInAnimation)
        {
            isInAnimation = true;
            attackHitbox.SetActive(true);

            ResetInvoke("ResetAttackType", 2);
            animator.SetFloat("attackType", attackType);
            attackType = attackType >= attackTypeCount ? 0 : attackType + 1;

            animator.SetTrigger("attack");

            Invoke("ResetAnimation", attackClip.length/2);
            Invoke("DisableAttackHitbox", attackClip.length/2.1f);
        }
    }

    private void ResetAnimation()
    {
        isInAnimation = false;
    }

    void DisableAttackHitbox()
    {
        attackHitbox.SetActive(false);
    }

    void ResetAttackType()
    {
        attackType = 0;
    }

    void ChangeHealth(int amount)
    {
        curHealth = Mathf.Clamp(curHealth - amount, 0, baseHealth);
        healthBar.value = (float)curHealth / baseHealth;
    }

    IEnumerator GetHit()
    {
        AnimationClip getHitClip = GetAnimationClip("GetHit");
        animator.SetTrigger("getHit");

        yield return new WaitForSeconds(getHitClip.length);
        isInAnimation = false;
    }

    public void GainCoin(int value)
    {
        coinHave += value;
        coinText.text = coinHave.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.layer == LayerMask.NameToLayer("EnemyMeleeAttackHitbox"))
        {
            EnemyController enemy = collidedObject.GetComponentInParent<EnemyController>();
            ChangeHealth(enemy.Damage);
            StartCoroutine(GetHit());
        }

        if (collidedObject.layer == LayerMask.NameToLayer("EnemyRangeAttackHitbox"))
        {
            Projectile enemyProjectile = collidedObject.GetComponent<Projectile>();
            ChangeHealth(enemyProjectile.Damage);
            StartCoroutine(GetHit());
        }
    }
}
