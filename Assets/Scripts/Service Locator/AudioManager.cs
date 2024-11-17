using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound(string clipName)
    {
        audioSource.clip = Resources.Load<AudioClip>(clipName);
        audioSource.Play();
    }
    public void PlaySoundOneShot(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>(clipName);
        audioSource.PlayOneShot(clip);
    }

    public void PauseSound()
    {
        audioSource.Pause();
    }

    public void UnpauseSound()
    {
        audioSource.UnPause();
    }
}
