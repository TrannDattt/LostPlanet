using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action OnLevelStarted;
    public event Action OnLevelCleared;

    private void Start()
    {
        ReturnHome();
    }

    public async Task StartLevel(string levelName)
    {
        await LoadingScreen.Instance.StartLoading();

        var loadOperation = SceneManager.LoadSceneAsync(levelName);
        while (!loadOperation.isDone)
        {
            await Task.Yield();
        }

        await LoadingScreen.Instance.StopLoading();

        OnLevelStarted?.Invoke();
        Time.timeScale = 1.0f;
        GameAudioManager.Instance.UnpauseBackground();
        PlayerPrefs.SetString("LevelSave", levelName);
    }

    public async void NewGame()
    {
        await StartLevel("Map1-1");
        Player.Instance.gameObject.SetActive(true);
        Player.Instance.Init();
    }

    public async void LoadSavedLevel()
    {
        if(PlayerPrefs.GetString("LevelSave") == "Map1-1")
        {
            NewGame();
        }
        else
        { 
            await StartLevel(PlayerPrefs.GetString("LevelSave")); 
        }
        Player.Instance.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
    }

    public async void ReturnHome()
    {
        await LoadingScreen.Instance.StartLoading();
        Player.Instance.gameObject.SetActive(false);

        var loadOperation = SceneManager.LoadSceneAsync("StartMenu");
        while (!loadOperation.isDone)
        {
            await Task.Yield();
        }

        await LoadingScreen.Instance.StopLoading();
    }

    public void LevelFailed()
    {
        Time.timeScale = 0;
        GameAudioManager.Instance.PauseBackground();
        GameAudioManager.Instance.PlaySFX(GameAudioManager.EAudio.StageFailedSFX);
        DiedScreen.Instance.FadeIn();
    }

    public async void LevelCleared()
    {
        GameAudioManager.Instance.PauseBackground();
        GameAudioManager.Instance.PlaySFX(GameAudioManager.EAudio.StageCompletedSFX);
        await Task.Delay(2000);
        OnLevelCleared?.Invoke();
    }

    public void StartDialog()
    {
        //gameUIManager.AcivateScene(gameUIManager.dialogScene);
        Time.timeScale = 0f;
        //dialogManager.StartDialog();
    }

    public void StopDialog() 
    {
        //dialogManager.StopDialog();
        Time.timeScale = 1f;
        //gameUIManager.DisableScene(gameUIManager.dialogScene);
    }
}
