using System.Threading;
using UnityEngine;

public class FireTrapScript : MonoBehaviour
{
    private ParticleSystem fireParticleSystem;
    private AudioSource audio;
    private float timer;
    [SerializeField] private float startTime = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = startTime;
        fireParticleSystem = GetComponentInChildren<ParticleSystem>();
        audio = GetComponentInChildren<AudioSource>();
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
        }
    }

    private void StartFlames()
    {
        if (!fireParticleSystem.isPlaying)
        {
            fireParticleSystem.Play();
            StartTimer();

            //fire sound


        }
    }

    private void StartTimer()
    {
        timer = startTime;
    }
}
