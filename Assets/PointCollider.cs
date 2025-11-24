using UnityEngine;

public class PointCollider : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private float radius = 0.05f;

    void Update()
    {
        bool touchingGround = Physics.CheckSphere(transform.position, radius, groundLayer);
    }
}
