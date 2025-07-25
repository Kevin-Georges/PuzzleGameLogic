using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;
    public float jumpTime = 0.35f;
    private bool isJumping;
    public float jumpTimeCounter;

    private Animator animator;

    void Start()
    {
        // Initialize Rigidbody2D, SpriteRenderer, and Animator components
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Horizontal input and sprite flipping
        input = Input.GetAxisRaw("Horizontal");
        if (input < 0)
        {
            spriteRenderer.flipX = true; // Flip sprite horizontally
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }

        // change player size
        transform.localScale = new Vector3(8, 8, 8);

        // Ground check for jumping
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);

        // Jump logic
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        // Animator parameters
        animator.SetFloat("xVelocity", Mathf.Abs(playerRb.velocity.x)); // Pass horizontal speed
        animator.SetFloat("yVelocity", playerRb.velocity.y);           // Pass vertical speed
        animator.SetBool("isJumping", !isGrounded);                    // Set jumping state
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
    }
}
