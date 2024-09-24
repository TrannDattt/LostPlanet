using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int damage;
    public int Damage { get { return damage; } set{damage = value;}}

    [SerializeField] float initClipTime;
    [SerializeField] float enableHitboxTime;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(enableHitboxTime);
        GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(initClipTime - enableHitboxTime);
        Destroy(gameObject);
    }
}
