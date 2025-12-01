using System;
using UnityEngine;
using Random = System.Random;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource gravitySource;
    private bool changedDir;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gravitySource = GameObject.Find("GravitySound").GetComponent<AudioSource>();
    }

    public void PlayFootStep()
    {
        float randomVolume = UnityEngine.Random.Range(0.2f, 0.5f);
        float randomPitch = UnityEngine.Random.Range(0.8f, 1.2f);
        
        audioSource.volume = randomVolume;
        audioSource.pitch = randomPitch;

        audioSource.Play();
    }

    public void PlayGravityFlip()
    {
        Debug.Log("Playing sound");
        gravitySource.Play();
    }
}
