using UnityEngine;

public class DartProjectileScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.up * speed);
    }
}
