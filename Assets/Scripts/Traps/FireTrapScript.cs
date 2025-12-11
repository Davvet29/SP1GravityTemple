using System.Threading;
using UnityEngine;

public class FireTrapScript : MonoBehaviour
{
    private ParticleSystem fireParticleSystem;
    [SerializeField] private Animator animator;
    private AudioSource audio;
    static float audioTimer;
    private float timer;
    [SerializeField] private float fireInterval = 5;
    [SerializeField] private float offset;
    private GameObject fireCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = fireInterval + offset;
        fireParticleSystem = GetComponentInChildren<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        fireCollider = transform.GetChild(1).gameObject;
    }


    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            StartFlames();
            PlaySound();
        }
    }

    private void StartFlames()
    {
        if (!fireParticleSystem.isPlaying)
        {
            StartTimer();
        }
    }

    private void StartTimer()
    {
        timer = fireInterval;
    }
    private void PlaySound()
    {
        animator?.Play("FireTrapActive");
        if (Time.time - FireTrapScript.audioTimer > 0.005f)
        {
            audio.Play();
            FireTrapScript.audioTimer =  Time.time;
        }
    }
}
