using System;
using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            animator.SetBool("IsClose", true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("IsClose", false);
        }
    }

    public void PlaySpearSound()
    {
        audio.Play();
    }
}
