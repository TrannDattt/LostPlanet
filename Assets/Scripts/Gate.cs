using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public string levelName;
    private bool isTriggered;

    public async void Navigating()
    {
        if(!isTriggered)
        { 
            isTriggered = true;
            await GameManager.Instance.StartLevel(levelName); 
        }
    }
}
