using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : AUnitUI
{
    [SerializeField] private RectTransform hpBarTransform;

    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        hpBarTransform.localScale = new Vector3(Mathf.Sign(enemy.MoveDir.x), 1, 1);
    }
}
