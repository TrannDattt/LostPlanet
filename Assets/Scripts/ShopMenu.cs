using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : Singleton<ShopMenu>
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isOpened;

    public void OpenShop()
    {
        isOpened = true;
        animator.SetBool("IsOpen", isOpened);
    }

    public void CloseShop()
    {
        isOpened = false;
        animator.SetBool("IsOpen", isOpened);
    }
}
