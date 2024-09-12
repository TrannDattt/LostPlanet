using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement
    Rigidbody2D playerRb;
    [SerializeField] float baseSpeed = 2f;
    float curSpeed;
    int lookDirection = 1;

    // Jump
    bool onGround = true;
    [SerializeField] float jumpForce = 20;

    // Health
    [SerializeField] int baseHealth = 100;
    int curHealth;

    // Attack
    bool canAttack = true;
    public int baseDamage = 10;
    float attackCd = 0.5f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] GameObject attackHitbox;
    int attackType = 0;
    int attackTypeCount = 2;

    // Die
    bool isDeath = false;

    // Action
    public InputAction moveAction;
    public InputAction attackAction;
    public InputAction jumpAction;

    // Animation
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


        moveAction.Enable();

        attackAction.Enable();
        attackAction.performed += Attack;

        jumpAction.Enable();
        jumpAction.performed += Jump;


        attackHitbox.SetActive(false);
        curHealth = baseHealth;
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
            playerRb.simulated = false;
        }


        Vector2 vectorLookDirection = moveAction.ReadValue<Vector2>();

        // Run + Change direction
        if(!Mathf.Approximately(vectorLookDirection.x, 0))
        {
            lookDirection = vectorLookDirection.x > 0 ? 1 : -1;
            curSpeed = baseSpeed * Mathf.Abs(lookDirection);
        }
        else
        {
            curSpeed = 0;
        }
        transform.localScale = new Vector2(lookDirection, 1);


        playerRb.position += curSpeed * lookDirection * Time.deltaTime * Vector2.right;
        animator.SetFloat("speed", curSpeed);
    }

    private void FixedUpdate()
    {

    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (canAttack)
        {
            canAttack = false;
            attackHitbox.SetActive(true);

            CancelInvoke("ResetAttackType");
            animator.SetFloat("attackType", attackType);
            attackType = attackType >= attackTypeCount ? 0 : attackType + 1;

            animator.SetTrigger("attack");

            Invoke("ResetAttack", attackCd);
            Invoke("DisableAttackHitbox", attackCd);
            Invoke("ResetAttackType", 2);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    void DisableAttackHitbox()
    {
        attackHitbox.SetActive(false);
    }

    void ResetAttackType()
    {
        attackType = 0;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (onGround)
        {
            playerRb.AddForce(Vector2.up * jumpForce);
            animator.SetTrigger("jump");
        }
    }

    void ChangeHealth(int amount)
    {
        curHealth -= amount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide with something");
        GameObject collidedObject = collision.collider.gameObject;

        if (collidedObject.layer == LayerMask.NameToLayer("EnemyAttackHitbox"))
        {
            EnemyController enemy = collidedObject.GetComponentInParent<EnemyController>();
            ChangeHealth(enemy.baseDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide with something2s");
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.layer == LayerMask.NameToLayer("EnemyAttackHitbox"))
        {
            EnemyController enemy = collidedObject.GetComponentInParent<EnemyController>();
            ChangeHealth(enemy.baseDamage); Debug.Log(curHealth);
        }
    }
}
