using UnityEngine;
using UnityEngine.Events;

public class DetectionColliderScript : MonoBehaviour
{
    public UnityEvent ActivateSpikeTrap;
    private AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ActivateSpikeTrap.Invoke();
            audio.Play();
        }
    }
}
