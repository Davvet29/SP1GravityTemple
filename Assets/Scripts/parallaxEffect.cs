using UnityEngine;
using UnityEngine.UIElements;

public class parallaxEffect : MonoBehaviour
{
    private Vector2 startPos;
    public GameObject cam;
    public float parallaxEffectSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceX = cam.transform.position.x * parallaxEffectSpeed;
        float distanceY = cam.transform.position.y * parallaxEffectSpeed;
        transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, transform.position.z);
    }
}
