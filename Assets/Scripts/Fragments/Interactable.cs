using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Canvas interactCanvas;
    [SerializeField] private UnityEvent callback;

    private void Awake()
    {
        interactCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            callback?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactCanvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactCanvas.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        callback = null;
    }
}
