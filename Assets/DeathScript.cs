using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private CapsuleCollider2D collider;
    private Transform playerTransform;
    private Transform currentResetPoint;
    [SerializeField] private List<GameObject> resetPoints = new(); 
    void Start()
    {
        resetPoints.AddRange(GameObject.FindGameObjectsWithTag("ResetPoint"));
        Debug.Log(resetPoints.Count);
        collider = GetComponent<CapsuleCollider2D>();
        playerTransform = GetComponent<Transform>();
        currentResetPoint = resetPoints[0].transform;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(currentResetPoint.transform.position);
        if(collision.gameObject.tag == "Enemy")
        {
            playerTransform = currentResetPoint;
        }
    }
}
