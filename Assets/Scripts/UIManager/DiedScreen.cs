using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiedScreen : Singleton<DiedScreen>
{
    [SerializeField] private Animator animator;

    public void FadeIn()
    {
        animator.SetBool("FadeIn", true);
    }

    public void FadeOut()
    {
        animator.SetBool("FadeIn", false);
    }
}
