using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathScript : MonoBehaviour
{

    public UnityEvent ReachedEnd;
    public UnityEvent ResetPlayer;
    private Transform playerTransform;
    private Transform currentResetPoint;
    private int currentResetPointIndex = 0;
    [SerializeField] private List<GameObject> resetPoints = new();
    private Rigidbody2D rb;
    [SerializeField]
    private PlayerMovement playerMovementScript;

    private float deathTimer;
    private bool dead = false;
    [SerializeField] private GameObject bloodEffect;
    void Start()
    {
        resetPoints.AddRange(GameObject.FindGameObjectsWithTag("ResetPoint"));
        playerTransform = GetComponent<Transform>();
        playerMovementScript = GetComponent<PlayerMovement>();
        currentResetPoint = resetPoints[currentResetPointIndex].transform;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (dead == true)
        {
            deathTimer -= Time.deltaTime;
        }
        if (dead == true && deathTimer < 0)
        {
            dead = false;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Reset();
        }

    }

    private void Death()
    {
        deathTimer = 0.5f;
        dead = true;
        bloodEffect.SetActive(true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        playerMovementScript.controlEnabled = false;
    }

    private void Reset()
    {
        playerTransform.position = currentResetPoint.position;
        bloodEffect.SetActive(false);
        ResetPlayer?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        }

        if (collision.gameObject.tag == "ResetPoint")
        {
            currentResetPoint = collision.transform;
        }
        if (collision.gameObject.tag == "WinPoint")
        {
            currentResetPoint = collision.transform;
            ReachedEnd.Invoke();
        }
    }
}
