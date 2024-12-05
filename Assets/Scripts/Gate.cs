using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public string levelName;

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated)
        {
            isActivated = true;
            ServiceLocator.Instance.NavigateToLevel(levelName);
        }
    }
}
