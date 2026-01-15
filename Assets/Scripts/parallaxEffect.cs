using UnityEngine;

public class parallaxEffect : MonoBehaviour
{
    private float startPos;
    public GameObject cam;
    public float parallaxEffectSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffectSpeed;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
