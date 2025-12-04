using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private Transform playerTransform;
    private Transform currentResetPoint;
    private int currentResetPointIndex = 0;
    [SerializeField] private List<GameObject> resetPoints = new(); 
    void Start()
    {
        resetPoints.AddRange(GameObject.FindGameObjectsWithTag("ResetPoint"));
        playerTransform = GetComponent<Transform>();
        currentResetPoint = resetPoints[currentResetPointIndex].transform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            playerTransform.position = currentResetPoint.position;
        }

        if (collision.gameObject.tag == "ResetPoint")
        {
            currentResetPoint = collision.transform;
        }
    }
}
