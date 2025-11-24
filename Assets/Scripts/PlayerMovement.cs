
using System;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class PlayerMovement : MonoBehaviour
/////////////// INFORMATION ///////////////
// This script automatically adds a Rigidbody2D, CapsuleCollider2D and CircleCollider2D component in the inspector.
// The Rigidbody2D component should (probably) have some constraints: Freeze Rotation Z
// The Circle Collider 2D should be set to "is trigger", resized and moved to a proper position for ground check.
// The following components are also needed: Player Input
// Gravity for the project is set in Unity at Edit -> Project Settings -> Physics2D -> Gravity Y
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool controlEnabled { get; set; } = true; // You can edit this variable from Unity Events
    private Vector2 moveInput;
    private Rigidbody2D rb;

    // Platformer specific variables
    [SerializeField] private LayerMask groundLayer; // ~0 is referring to EVERY layer. Do you want a specific layer? Serialize the variable and assign the Layer of your choice.
    private Vector2 velocity;
    private bool wasGrounded;
    private bool isGrounded;
    private bool isMoving;
    private bool changedDir;
    [SerializeField] private Animator animator;
    private int gravityDirection = 1;
    private int gravityFlips = 1;
    private float acceleration;
    [SerializeField] private float walkAcceleration;
    private float newPos;
    private float newDir;
    private float oldPos;
    private float oldDir;
    private Transform topCollider;
    private Transform bottomCollider;
    //public List<AudioClip> sounds = new List<AudioClip>();
    void Awake()
    {
        Transform transformTop = transform.GetChild(1);
        topCollider = transformTop.GetComponent<Transform>();
        Transform transformBottom = transform.GetChild(2);
        bottomCollider = transformBottom.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        // Set gravity scale to 0 so player won't "fall"
        rb.gravityScale = 0;
        animator = GetComponent<Animator>();
        newPos = transform.position.x;
        walkAcceleration = 1f;
    }

    void Update()
    {
        velocity = TranslateInputToVelocity(moveInput);
        // Check if character gained contact with ground this frame
        if (isGrounded == true && wasGrounded != true)
        {
            gravityFlips = 1;
        }
        wasGrounded = isGrounded;
        // Flip sprite according to direction (if a sprite renderer has been assigned)
        if (spriteRenderer)
        {
            if (moveInput.x > 0.01f)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveInput.x < -0.01f)
            {
                spriteRenderer.flipX = false;
            }
            if (gravityDirection == -1)
            {
                spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.flipY = false;
            }
        }
    }
    private void FixedUpdate()
    {
        oldDir = newDir;
        oldPos = newPos;
        newDir = GetDir();
        newPos = transform.position.x;
        isGrounded = IsGrounded();
        isMoving = IsMoving();
        changedDir = ChangedDirection();
        ApplyGravity();
        rb.linearVelocity = velocity;
        if (isMoving)
        {
            animator.SetBool("Walking", true);
            walkAcceleration *= 1.05f;
            if (walkAcceleration >= 2)
            {
                walkAcceleration = 2;
            }
        }
        else if (!isMoving || changedDir)
        {
            animator.SetBool("Walking", false);
            walkAcceleration = 1f;
        }
    }
    private bool LayerCollider(Transform colliderTransform)
    {
        return Physics2D.OverlapCircle(colliderTransform.position, 0.05f, groundLayer);
    }
    private bool IsGrounded()
    {
        if (
            (gravityDirection == 1 && LayerCollider(bottomCollider))
            || (gravityDirection == -1 && LayerCollider(topCollider))
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsMoving()
    {
        if (newPos != oldPos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void ApplyGravity()
    {
        // Applies a set gravity for when player is grounded
        if (isGrounded)
        {
            acceleration = 4;
            velocity.y = gravityDirection * -1.0f;
        }
        // Updates fall speed with gravity if object isn't grounded
        else
        {
            acceleration *= 1.05f;
            if (acceleration >= 10)
            {
                acceleration = 10;
            }
            velocity.y += gravityDirection * (Physics2D.gravity.y * acceleration * Time.deltaTime);
            velocity.y = Mathf.Clamp(velocity.y,-60, 60);
        }
    }
    private float GetDir()
    {
        if (velocity.x > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    private bool ChangedDirection()
    {
        if (newDir != oldDir)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Vector2 TranslateInputToVelocity(Vector2 input)
    {
        // Make the character move along the X-axis
        return new Vector2(input.x * maxSpeed * (walkAcceleration / 2), velocity.y);
    }
    // Handle Move-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        if (controlEnabled)
        {
            moveInput = context.ReadValue<Vector2>().normalized;
        }
        else
        {
            moveInput = Vector2.zero;
        }
    }
    // Handle Jump-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && controlEnabled && gravityFlips >= 0)
        {
            Debug.Log("Jump!");
            acceleration = 4f;
            velocity.y = 3;
            gravityDirection *= -1;
            gravityFlips--;
        }
    }
}