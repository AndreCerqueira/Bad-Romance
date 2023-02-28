using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float jumpForce = 14f;
    public Transform shootingPoint;
    public float moveSpeed = 7f;
    public float gravityScale = 5f;
    public float fallingGravityScale = 12f;

    // New Jump System
    [Header("New Jump System")]
    [SerializeField] float jumpTime = 0.4f;
    [SerializeField] float fallMultiplier = 5f;
    [SerializeField] float jumpMultiplier = 3f;
    Vector2 vecGravity;
    float jumpCounter;
    bool isJumping;

    // gun audio
    public AudioClip gunAudio;

    private bool isFacingRight = true;

    public Transform groundCheck;
    [SerializeField]
    bool isGrounded;

    public LayerMask groundLayer;

    bool jumpRequest;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        vecGravity = new Vector2(0, -Physics.gravity.y);
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.2f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        // flip the sprite
        if (moveInput > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (moveInput < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpCounter = 0;
        }

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }

        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
            {
                isJumping = false;
            }

            float t = jumpCounter / jumpTime;
            float currentJumpMultiplier = jumpMultiplier;

            if (t == 0.5f)
            {
                currentJumpMultiplier = jumpMultiplier * (1 - t);
            }
            
            rb.velocity += vecGravity * currentJumpMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        // check if the player is running
        if (moveInput == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }


    }

// play gunAudio
    public void PlayGunAudio()
    {
        AudioSource.PlayClipAtPoint(gunAudio, transform.position);
    }

    /*
        void OnCollisionEnter2D(Collision2D collision)
        {
            ContactPoint2D[] points = new ContactPoint2D[2];
            collision.GetContacts(points);
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].normal == Vector2.up && !groundTouched.Contains(collision.collider))
                {
                    groundTouched.Add(collision.collider);
                    return;
                }
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (groundTouched.Contains(collision.collider))
                groundTouched.Remove(collision.collider);
        }
        */
}
