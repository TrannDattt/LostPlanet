using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip dieSound;
    public AudioClip hurtSound;
    public AudioClip runSound;
    public AudioClip attackSound;
    public AudioClip specialAttackSound;
    public AudioClip dashSound;

    public void PlayDieSound()
    {
        audioSource.PlayOneShot(dieSound);
    }

    public void PlayRunSound()
    {
        audioSource.clip = runSound;
        audioSource.Play();
    }

    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(hurtSound);
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlaySpecialAttackSound()
    {
        audioSource.PlayOneShot(specialAttackSound);
    }

    public void PlayDashSound()
    {
        audioSource.PlayOneShot(dashSound);
    }
}
