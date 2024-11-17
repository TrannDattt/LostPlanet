using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject pauseScene;
    public GameObject dieScene;

    void Awake()
    {
        DisableScene(pauseScene);
        DisableScene(dieScene);
    }

    public void AcivateScene(GameObject scene)
    {
        scene.SetActive(true);
    }

    public void DisableScene(GameObject scene)
    {
        scene.SetActive(false);
    }

    public void ChangeCharacterHealth(AController character, float curHealth, float maxHealth)
    {

    }
}
