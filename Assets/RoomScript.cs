using Unity.Cinemachine;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField] private GameObject CMCamera;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CMCamera.SetActive(true);
            Debug.Log("CameraSwiched");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CMCamera.SetActive(false);
            Debug.Log("CameraSwiched");
        }
    }
}
