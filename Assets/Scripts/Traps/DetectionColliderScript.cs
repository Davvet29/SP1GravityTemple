using UnityEngine;
using UnityEngine.Events;

public class DetectionColliderScript : MonoBehaviour
{
    public UnityEvent ActivateSpikeTrap;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ActivateSpikeTrap.Invoke();
        }
    }
}
