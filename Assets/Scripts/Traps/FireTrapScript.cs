using System.Threading;
using UnityEngine;

public class FireTrapScript : MonoBehaviour
{
    private ParticleSystem fireParticleSystem;
    private AudioSource audio;
    private float timer;
    [SerializeField] private float fireInterval = 5;
    [SerializeField] private float offset;
    private GameObject fireCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = fireInterval + offset;
        fireParticleSystem = GetComponentInChildren<ParticleSystem>();
        audio = GetComponentInChildren<AudioSource>();
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
        }

        if(!fireParticleSystem.isPlaying)
        {
            fireCollider.SetActive(false);
        }
    }

    private void StartFlames()
    {
        if (!fireParticleSystem.isPlaying)
        {
            fireParticleSystem.Play();
            fireCollider.SetActive(true);
            StartTimer();
            audio.Play();
        }
    }

    private void StartTimer()
    {
        timer = fireInterval;
    }
}
