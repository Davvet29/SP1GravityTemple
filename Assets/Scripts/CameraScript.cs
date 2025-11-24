using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    private float staticPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        staticPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, staticPosition, transform.position.z);
    }
}
