using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathScript : MonoBehaviour
{
    
    public UnityEvent ReachedEnd;
    private Transform playerTransform;
    private Transform currentResetPoint;
    private int currentResetPointIndex = 0;
    [SerializeField] private List<GameObject> resetPoints = new(); 
    private Rigidbody2D rb;

    private float deathTimer;
    private bool dead = true;
    void Start()
    {
        resetPoints.AddRange(GameObject.FindGameObjectsWithTag("ResetPoint"));
        playerTransform = GetComponent<Transform>();
        currentResetPoint = resetPoints[currentResetPointIndex].transform;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() 
    {
        if(dead == true)
        {
            deathTimer-= Time.deltaTime;
        }
        if(dead = true && deathTimer < 0)
        {
            dead = false;
            //rb.FreezePositionX;
            playerTransform = currentResetPoint;
        }

    }

    private void Death()
    {

        deathTimer = 1;
        dead = true;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Death();
        }

        if (collision.gameObject.tag == "ResetPoint")
        {
            currentResetPoint = collision.transform;
        }
        if(collision.gameObject.tag == "WinPoint")
        {
            currentResetPoint = collision.transform;
            ReachedEnd.Invoke();
        }
    }
}
