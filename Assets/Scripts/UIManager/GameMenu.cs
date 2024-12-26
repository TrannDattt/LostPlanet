using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isOpened;

    private void Awake()
    {
        if (isOpened)
        {
            animator.SetBool("IsOpen", isOpened);
        }
    }

    public void ChangMenuStatus()
    {
        isOpened = !isOpened;
        animator.SetBool("IsOpen", isOpened);
    }
}
