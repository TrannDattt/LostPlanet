using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : AnimState
{
    public GameObject hurtEffect;
    public AnimationClip hurtEffectClip;

    private Animator hurtEffectAnimator;

    public override void EnterState()
    {
        base.EnterState();

        //CharacterAudio.PlayHurtSound();
        StartCoroutine(PopHurtEffect());
    }

    public override void ExitState()
    {
        base .ExitState();

        hurtEffect.SetActive(false);
    }

    private void Start()
    {
        hurtEffectAnimator = hurtEffect.GetComponent<Animator>();
        hurtEffect.SetActive(false);
    }

    private IEnumerator PopHurtEffect()
    {
        hurtEffect.SetActive(true);
        yield return null;
        hurtEffectAnimator.Play(hurtEffectClip.name);
    }
}
