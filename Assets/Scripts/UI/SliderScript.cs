using System;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private float volume;
    public string track;

    void Start()
    {
        volume = PlayerPrefs.GetFloat(track, 0.75f);
        slider.value = volume;
    }

    public void SetVolume(string track, float value)
    {
        audioMixer.SetFloat(track, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f) * 20f));
        PlayerPrefs.SetFloat(track, value);
    }

}
