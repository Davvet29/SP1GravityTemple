using UnityEngine;
using UnityEngine.UIElements;

public class DartProjectileScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed;
    public bool isFlipped;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isFlipped)
        {
            transform.RotateAround(transform.position, transform.up, 180f);
        }
        rb.linearVelocity = transform.right * speed;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
