using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : Singleton<GameAudioManager>
{
    [SerializeField] private AudioSource backgrounAudio;
    [SerializeField] private AudioSource sfxAudio;

    [SerializeField] private AudioClip backgroundMusic;

    [SerializeField] private AudioClip stageCompletedSFX;
    [SerializeField] private AudioClip stageFailedSFX;
    [SerializeField] private AudioClip btnClickedSFX;

    public enum EAudio
    {
        BackgroundMusic,
        StageCompletedSFX,
        StageFailedSFX,
        BtnClickedSFX,
    }

    private Dictionary<EAudio, AudioClip> audioClipDict = new();

    protected override void Awake()
    {
        base.Awake();

        audioClipDict.Add(EAudio.BackgroundMusic, backgroundMusic);
        audioClipDict.Add(EAudio.StageCompletedSFX, stageCompletedSFX);
        audioClipDict.Add(EAudio.StageFailedSFX, stageFailedSFX);
        audioClipDict.Add(EAudio.BtnClickedSFX, btnClickedSFX);
    }

    private void Start()
    {
        PlayBackground(EAudio.BackgroundMusic);
    }

    public void PlayBackground(EAudio audioKey)
    {
        backgrounAudio.clip = audioClipDict[audioKey];
        backgrounAudio.Play();
    }

    public void PlaySFX(EAudio audioKey)
    {
        //sfxAudio.
        sfxAudio.PlayOneShot(audioClipDict[audioKey]);
    }

    public void ChangeBackgroundVolume(float value) => backgrounAudio.volume = value;
    public void ChangeSFXVolume(float value) => sfxAudio.volume = value;
    public void PauseBackground() => backgrounAudio.Pause();
    public void UnpauseBackground() => backgrounAudio.UnPause();
}
