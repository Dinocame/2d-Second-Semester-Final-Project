using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 5f;
    public float speed = 2f;

    [Header("Detection")]
    public float closeDetectRadius = 2f;
    public float visionRadius = 6f;
    public LayerMask playerLayer;

    [Header("Ground Check")]
    public float groundCheckDistance = 1.5f;
    public LayerMask groundLayer;
    public float edgeOffset = 0.5f;

    private Vector3 startPosition;
    private int direction = 1;
    private Rigidbody2D rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckBounds();
        HandleFacing();
        DetectPlayer();
        CheckGroundAhead();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void CheckBounds()
    {
        float offset = transform.position.x - startPosition.x;

        if (offset >= moveDistance)
            direction = -1;
        else if (offset <= -moveDistance)
            direction = 1;
    }

    void HandleFacing()
    {
        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }


    //PLAYER DETECTION

    void DetectPlayer()
    {
        // 1. Close circle (around enemy)
        Collider2D closeHit = Physics2D.OverlapCircle(transform.position, closeDetectRadius, playerLayer);

        if (closeHit != null)
        {
            Debug.Log("Player VERY close");
        }

        // 2. Forward vision circle
        Vector2 forwardPos = (Vector2)transform.position + Vector2.right * direction * (visionRadius / 2);

        Collider2D visionHit = Physics2D.OverlapCircle(forwardPos, visionRadius, playerLayer);

        if (visionHit != null)
        {
            Debug.Log("Player in front vision");
        }
    }


    //  GROUND CHECK (EDGE STOP)

    void CheckGroundAhead()
    {
        // Offset forward depending on direction
        Vector2 origin = (Vector2)transform.position + new Vector2(direction * edgeOffset, 0);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider == null)
        {
            // No ground ahead → turn around
            direction *= -1;
        }

        // Debug line
        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, Color.yellow);
    }


    // DRAW GIZMOS (EDITOR ONLY)

    void OnDrawGizmos()
    {
        // Close detection
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeDetectRadius);

        // Forward vision
        Gizmos.color = Color.blue;
        Vector2 forwardPos = (Vector2)transform.position + Vector2.right * direction * (visionRadius / 2);
        Gizmos.DrawWireSphere(forwardPos, visionRadius);
    }
}