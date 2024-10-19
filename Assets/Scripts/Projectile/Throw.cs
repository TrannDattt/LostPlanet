using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : AProjectile
{
    public float flySpeed;

    protected override void Start()
    {
        ModifyInitAnimation();
    }

    protected override void Update()
    {
        base.Update();

        FlyToward();
    }

    void FlyToward()
    {
        Vector2 dir = destinatePos - spawnPos;
        Vector2 dirNormalize = dir / Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

        body.transform.Translate(flySpeed * Time.deltaTime * dirNormalize); Debug.Log(dirNormalize);
    }

    void ModifyInitAnimation()
    {
        Vector2 dir = destinatePos - spawnPos;
        body.transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);

        float flyTime = dir.magnitude / flySpeed;

        initState.playSpeed = 1 / flyTime;
    }
}
