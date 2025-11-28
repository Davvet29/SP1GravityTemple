using UnityEngine;

public class DartTrapScript : MonoBehaviour
{
    [SerializeField] private GameObject dart;
    private Transform dartTransform;
    private float timer;
    [SerializeField] private float startTime = 5;
    void Start()
    {
        dartTransform = GetComponentInChildren<Transform>();
    }

    void Update()
    {
        Debug.Log(dartTransform.position);
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SpawnDart();
        }
    }

    private void SpawnDart()
    {
        GameObject tempdart = Instantiate(dart, transform.position, transform.rotation);
        tempdart.transform.position += dartTransform.position;
        StartTimer();
    }

    private void StartTimer()
    {
        timer = startTime;
    }
}
