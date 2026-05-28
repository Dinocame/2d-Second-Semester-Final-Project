using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    public AudioSource footstepSource;
    public AudioClip footstepClip;
    public float footstepInterval = 0.4f;
    private float footstepTimer;
    

    private bool knockbackActive = false;
    private float lastGroundedTime;
    public float coyoteTime = 2f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck1;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private Transform groundCheck3;
    [SerializeField] private Transform groundCheck4;
    [SerializeField] private Transform groundCheck5;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
         
        horizontal = Input.GetAxisRaw("Horizontal");
        HandleFootsteps();
        
        //Record last time it was grounded
        if (IsGrounded() && Time.time>=coyoteTime)
        {
            lastGroundedTime = Time.time;
        }
        
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && Time.time - lastGroundedTime <= coyoteTime)
        {
            Debug.Log("Last " + lastGroundedTime);
            lastGroundedTime = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && rb.velocity.y > 0f)
        {
            lastGroundedTime = 0;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
        HandleFootsteps(); 
    }

    private void FixedUpdate()
    {
        if (!knockbackActive)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(groundCheck1.position, Vector2.down, 0.2f, groundLayer) || Physics2D.Raycast(groundCheck2.position, Vector2.down, 0.2f, groundLayer) || Physics2D.Raycast(groundCheck3.position, Vector2.down, 0.2f, groundLayer) || Physics2D.Raycast(groundCheck4.position, Vector2.down, 0.2f, groundLayer) || Physics2D.Raycast(groundCheck5.position, Vector2.down, 0.2f, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal >0f)
        {
            /*
             isFacingRight = !isFacingRight;
             Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            */
            if (horizontal > 0)
            {
                spriteRenderer.flipX = false;
                isFacingRight = true;
            }

            if (horizontal < 0)
            {
                spriteRenderer.flipX = true;
                isFacingRight = false;
            }
        }
    }
    public int GetFacingDirection()
    {
        return isFacingRight ? 1 : -1;
    }

    public void ApplyKnockback(Vector2 force, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(KnockbackRoutine(force, duration));
    }

    IEnumerator KnockbackRoutine(Vector2 force, float duration)
    {
        knockbackActive = true;

        rb.AddForce(force, ForceMode2D.Impulse);
        /*
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(force, ForceMode2D.Impulse);
        */
        yield return new WaitForSeconds(duration);

        knockbackActive = false;
    }
    void HandleFootsteps()
    {
        bool isMoving = Mathf.Abs(horizontal) > 0.1f;
        if (isMoving && IsGrounded())
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                footstepSource.PlayOneShot(footstepClip);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }
}
