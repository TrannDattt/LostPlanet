using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance {  get; private set; }

    protected virtual void Awake()
    {
        if(Instance != null && Instance != (T)this)
        {
            Destroy(this);
        }
        else
        {
            Instance = (T)this;
        }
    }
}
