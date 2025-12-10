
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
/////////////// INFORMATION ///////////////
// This script automatically adds a Rigidbody2D, CapsuleCollider2D and CircleCollider2D component in the inspector.
// The Rigidbody2D component should (probably) have some constraints: Freeze Rotation Z
// The Circle Collider 2D should be set to "is trigger", resized and moved to a proper position for ground check.
// The following components are also needed: Player Input
// Gravity for the project is set in Unity at Edit -> Project Settings -> Physics2D -> Gravity Y
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool controlEnabled { get; set; } = true; // You can edit this variable from Unity Events
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private AudioSource landingSource;
    private ParticleSystem[] topParticles = new ParticleSystem[2];
    private ParticleSystem[] botParticles = new ParticleSystem[2];

    // Platformer specific variables
    [SerializeField] private LayerMask groundLayer; // ~0 is referring to EVERY layer. Do you want a specific layer? Serialize the variable and assign the Layer of your choice.
    private bool wasGrounded;
    private bool isGrounded;
    private bool isMoving;
    [SerializeField] public bool changedDir;
    [SerializeField] private Animator animator;
    
    //gravity
    private int gravityDirection = 1;
    private int gravityFlips = 1;
    private bool gravityFlipped;
    [SerializeField] private float gravityAcceleration = 1f;
    [SerializeField] private float gravityCoefficient = 1.2f;
    [SerializeField] private float maxGravityAcceleration = 10f;

    public UnityEvent ChangeGravity;

    //walking
    [SerializeField] private float walkAcceleration = 1f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float maxAcceleration = 10f;
    [SerializeField] private float walkCoefficient = 1.05f;
    private Vector2 velocity;

    private float newPos;
    private float newDir;
    private float oldPos;
    private float oldDir;
    private float fallStart;
    private float fallDist;
    private Transform topCollider;
    private Transform bottomCollider;
    [SerializeField] private bool quickGravityFlip;
    [SerializeField] private bool floatyGravity;
    private bool spacePressedDown;

    void Awake()
    {
        topParticles = GameObject.Find("TopLandingParticles").GetComponentsInChildren<ParticleSystem>();
        botParticles = GameObject.Find("BotLandingParticles").GetComponentsInChildren<ParticleSystem>();
        landingSource = GameObject.Find("LandingSound").GetComponent<AudioSource>();
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

    private void Start()
    {
        fallStart = transform.position.y;
    }

    void Update()
    {
        velocity = TranslateInputToVelocity(moveInput);
        // Check if character gained contact with ground this frame
        if (isGrounded == true && wasGrounded != true)
        {
            gravityFlips = 1;
            
            //touch ground ljud
            float pitch;
            float volume;
            
            PlayLandingSound();
        }
        wasGrounded = isGrounded;
        // Flip sprite according to direction (if a sprite renderer has been assigned)
        if (spriteRenderer)
        {
            if (moveInput.x > 0.01f)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveInput.x < -0.01f)
            {
                spriteRenderer.flipX = true;
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
        UpdateDir();
        UpdatePos();
        isGrounded = IsGrounded();
        ApplyGravity();
        rb.linearVelocity = velocity;
        //resetar 
        if (isMoving)
        {
            animator.SetBool("Walking", true);
            walkAcceleration *= walkCoefficient;
            if (walkAcceleration >= maxAcceleration)
            {
                walkAcceleration = maxAcceleration;
            }
        }
        else if (!isMoving || changedDir)
        {
            animator.SetBool("Walking", false);
            velocity.x = 0;
            walkAcceleration = 1f;
        }
        if(gravityFlipped)
        {
            //sound
            ChangeGravity.Invoke();
        }
        gravityFlipped = false;
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
    private bool IsMoving(float _newPos, float _oldPos)
    {
        if (_newPos != _oldPos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void UpdatePos()
    {
        oldPos = newPos;
        newPos = transform.position.x;
        isMoving = IsMoving(oldPos, newPos);
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

    private void UpdateDir()
    {
        oldDir = newDir;
        newDir = GetDir();
        changedDir = ChangedDirection(oldDir, newDir);
    }
    private bool ChangedDirection(float _newDir, float _oldDir)
    {
        if (_newDir != _oldDir)
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
            gravityCoefficient = 1.2f;
            gravityAcceleration = 1f;
            velocity.y = gravityDirection * -1.0f;
        }
        // Updates fall speed with gravity if object isn't grounded
        else
        {
            UpdateCoefficient();
            gravityAcceleration *= gravityCoefficient;
            if (gravityAcceleration >= maxGravityAcceleration)
            {
                gravityAcceleration = maxGravityAcceleration;
            }

            SetVelocity();
        }
    }
    private void UpdateCoefficient()
    {
            if(spacePressedDown && gravityCoefficient <= 1.2f)
            {
                gravityCoefficient+= 0.0007f;
            }
            else if(!spacePressedDown && gravityCoefficient != 1.2f)
            {
                gravityCoefficient = 1.2f;
            }
    }
    private void SetVelocity()
    {
        velocity.y += gravityDirection * (Physics2D.gravity.y * gravityAcceleration * Time.deltaTime);
        velocity.y = Mathf.Clamp(velocity.y,-60, 60);
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

            if(quickGravityFlip && isGrounded)
            {
                gravityAcceleration = 3f;
            }
            else
            {
                gravityAcceleration = 1f;
            }
            if(floatyGravity && !isGrounded)
            {
                gravityCoefficient = 1f;
            }
            velocity.y = 1;
            gravityDirection *= -1;
            gravityFlips--;
            gravityFlipped = true;
            spacePressedDown = true;
        }
        if(context.canceled)
        {
            spacePressedDown = false;
        }
    }
    private void PlayLandingSound()
    {
        if (gravityDirection == -1)
        {
            foreach (ParticleSystem ps in topParticles)
            {
                Debug.Log("played topparticles");
                ps.Play();
            }
        }
        else if (gravityDirection == 1)
        {
            foreach (ParticleSystem ps in botParticles)
            {
                Debug.Log("played BOTparticles");
                ps.Play();
            } 
        }
        
        float pitch;
        float volume;
        fallDist = Mathf.Abs(transform.position.y - fallStart);
        fallStart = transform.position.y;
            
        if (fallDist <= 4f)
        {
            pitch = 0.65f;
            volume = 0.75f;
        }
        else
        {
            pitch = 1;
            volume = 0.9f;
        }
        landingSource.volume = volume;
        landingSource.pitch = pitch;
        landingSource.Play();
    }
}