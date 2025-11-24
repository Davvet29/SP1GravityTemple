using Unity.VisualScripting;
using UnityEngine;

public class PumpkinScript : MonoBehaviour
{
    [SerializeField] CounterScript PumpkinCounter;

    void OnTriggerEnter2D(Collider2D other)
    {
        PumpkinCounter.count++;
        Destroy(this.gameObject);
    }
}
