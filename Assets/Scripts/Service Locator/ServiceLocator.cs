using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceLocator : Singleton<ServiceLocator>
{
    public AudioManager audioManager;
    public GameUIManager gameUIManager;
    public DialogManager dialogManager;
    public MapManager mapManager;

    public void NewGame()
    {
        string firstMap = "Map1-1";

        PlayerPrefs.SetString("LevelSave", firstMap);
        SceneManager.LoadScene(firstMap);
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LevelSave"));
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Unpause();
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void NavigateToLevel(string nextLevelName)
    {
        PlayerPrefs.SetString("LevelSave", nextLevelName);

        SceneManager.LoadScene(nextLevelName);
    }

    public void PlayerDie()
    {
        Time.timeScale = 0;
        audioManager.PlaySoundOneShot("DieSound");
        gameUIManager.AcivateScene(gameUIManager.dieScene);
    }

    public void StartDialog()
    {
        //gameUIManager.AcivateScene(gameUIManager.dialogScene);
        Time.timeScale = 0f;
        dialogManager.StartDialog();
    }

    public void StopDialog() 
    {
        dialogManager.StopDialog();
        Time.timeScale = 1f;
        //gameUIManager.DisableScene(gameUIManager.dialogScene);
    }

    private void Start()
    {
        //Debug.Log(PlayerPrefs.GetInt("LevelSave"));
        //Debug.Log(SceneManager.sceneCountInBuildSettings);
        //StartDialog();
    }
}
