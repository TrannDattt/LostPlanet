using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : AUnitUI
{
    public override void SetInstance(AUnit unit)
    {
        base.SetInstance(unit);

        hpBar = TMPPooling.Instance.SpawnHpBar(transform.position + Vector3.up);
        hpBar.gameObject.SetActive(true);
        hpBar.value = 1;
    }

    private void FixedUpdate()
    {
        if(hpBar != null)
        { 
            hpBar.transform.position = transform.position + Vector3.up; 
        }
    }

    public void ReturnHpBarToPool()
    {
        //hpBar = null;
        hpBar.gameObject.SetActive(false);
        TMPPooling.Instance.ReturnHpBarToPool(hpBar);
    }
}
