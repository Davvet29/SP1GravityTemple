using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class DartTrapScript : MonoBehaviour
{
    [SerializeField] private GameObject dart;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject dartPositionObject;
    private Transform dartTransform;
    private float timer;
    [SerializeField] private float dartInterval = 5;
    [SerializeField] private float offset;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        timer = dartInterval + offset;
        audio.Play();
        dartTransform = dartPositionObject.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer.flipX)
        {
            dartTransform.position.Scale(new Vector3(-1,0,0));
        }
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SpawnDart();
        }
    }

    private void SpawnDart()
    {
        GameObject tempdart = Instantiate(dart, dartTransform.position, transform.rotation);
        tempdart.transform.SetParent(transform);
        if(spriteRenderer.flipX)
        {
            tempdart.GetComponent<DartProjectileScript>().isFlipped = true;
        }
        StartTimer();
    }

    private void StartTimer()
    {
        timer = dartInterval;
    }
}
