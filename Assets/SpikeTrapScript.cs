using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
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
}
