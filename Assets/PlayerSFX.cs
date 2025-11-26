using System;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootStep()
    {
        audioSource.Play();
    }
}
