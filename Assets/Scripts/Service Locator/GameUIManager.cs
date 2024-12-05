using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject pauseScene;
    public GameObject dieScene;
    //public GameObject dialogScene;

    void Awake()
    {
        DisableScene(pauseScene);
        DisableScene(dieScene);
        //DisableScene(dialogScene);
    }

    public void AcivateScene(GameObject scene)
    {
        scene.SetActive(true);
    }

    public void DisableScene(GameObject scene)
    {
        scene.SetActive(false);
    }
}
