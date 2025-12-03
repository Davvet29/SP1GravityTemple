using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audioSource;
    void Update()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeAudio(int i)
    {
        audioSource.clip = audioClips[i];
        audioSource.Play();
    }
}
