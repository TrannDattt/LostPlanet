using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadingScreen : Singleton<LoadingScreen>
{
    [SerializeField] private Animator animator;

    public async Task StartLoading(int waitTime = 1000)
    {
        animator.SetBool("Loading", true);
        await Task.Delay(waitTime);
    }

    public async Task StopLoading(int waitTime = 1000)
    {
        await Task.Delay(waitTime);
        animator.SetBool("Loading", false);
    }
}
