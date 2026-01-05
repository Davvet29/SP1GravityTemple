using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckpointScript : MonoBehaviour
{
    public bool IsActive;
    void Update()
    {
        gameObject.SetActive(IsActive);
    }
}
