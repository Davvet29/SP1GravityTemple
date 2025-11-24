using UnityEngine;

public class ButtonSFXScript : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void PlaySFX()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
