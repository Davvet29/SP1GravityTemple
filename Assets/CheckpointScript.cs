using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckpointScript : MonoBehaviour
{
    public int gravityDirection;
    public void ChangeState(bool active)
    {
        gameObject.SetActive(active);
    }
}
