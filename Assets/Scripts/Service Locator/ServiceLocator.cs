using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceLocator : Singleton<ServiceLocator>
{
    public AudioManager audioManager;
    public GameUIManager gameUIManager;

    public void NewGame()
    {
        PlayerPrefs.SetInt("LevelSave", 1);
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelSave"));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        audioManager.PauseSound();
        gameUIManager.AcivateScene(gameUIManager.pauseScene);
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
        audioManager.UnpauseSound();
        gameUIManager.DisableScene(gameUIManager.pauseScene);
    }

    public void Restart()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelSave"));
        Unpause();
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void NextLevel()
    {
        int curScene = PlayerPrefs.GetInt("LevelSave");
        int nextScene = curScene + 1 >= SceneManager.sceneCountInBuildSettings ? 0 : curScene + 1;

        PlayerPrefs.SetInt("LevelSave", nextScene);

        SceneManager.LoadScene(nextScene);
    }

    public void PlayerDie()
    {
        Time.timeScale = 0;
        audioManager.PlaySoundOneShot("DieSound");
        gameUIManager.AcivateScene(gameUIManager.dieScene);
    }

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("LevelSave"));
        //Debug.Log(SceneManager.sceneCountInBuildSettings);
    }
}
