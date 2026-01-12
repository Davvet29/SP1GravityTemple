using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckpointScript : MonoBehaviour
{
    public void ChangeState(bool active)
    {
        gameObject.SetActive(active);
    }
}
