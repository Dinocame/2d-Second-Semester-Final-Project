using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveDistance = 5f;
    public float speed = 2f;
    public float chaseSpeed = 3.5f;

    [Header("Detection")]
    public float closeDetectRadius = 2f;
    public float visionRadius = 6f;
    public LayerMask playerLayer;

    private float attackLockTimer = 0f;

    [Header("Ground Check")]
    public float groundCheckDistance = 1.5f;
    public LayerMask groundLayer;
    public float edgeOffset = 0.5f;

    private Vector3 startPosition;
    private int direction = 1;
    private Rigidbody2D rb;

    private Transform player;

    private enum State { Patrol, Chase, Return }
    private State currentState = State.Patrol;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DetectPlayer();
        HandleFacing();
    }

    void FixedUpdate()
    {
        if (attackLockTimer > 0)
        {
            attackLockTimer -= Time.fixedDeltaTime;
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        HandleState();
    }

    public void LockMovement(float duration)
    {
        attackLockTimer = duration;
    }

    void HandleState()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Return:
                ReturnToStart();
                break;
        }
    }

    void Patrol()
    {
        if (!HasGroundAhead())
        {
            direction *= -1;
        }

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        float offset = transform.position.x - startPosition.x;

        if (offset >= moveDistance)
            direction = -1;
        else if (offset <= -moveDistance)
            direction = 1;
    }

    void Chase()
    {
        if (player == null)
        {
            currentState = State.Return;
            return;
        }

        Vector2 dir = (player.position - transform.position).normalized;
        int moveDir = dir.x >= 0 ? 1 : -1;

        if (!HasGroundInDirection(moveDir))
        {
            direction = moveDir; // still face player
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        rb.velocity = new Vector2(dir.x * chaseSpeed, rb.velocity.y);
        direction = moveDir;

        if (!CanSeePlayer())
        {
            player = null;

            float dirToStart = (startPosition.x - transform.position.x);
            direction = dirToStart > 0 ? 1 : -1;
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            currentState = State.Return;
        }
    }

    void ReturnToStart()
    {
        Vector2 dir = (startPosition - transform.position);

        if (Mathf.Abs(dir.x) < 0.1f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            currentState = State.Patrol;
            return;
        }

        int moveDir = dir.x > 0 ? 1 : -1;

        if (!HasGroundInDirection(moveDir))
        {
            moveDir *= -1;
        }

        rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
        direction = moveDir;
    }   

    void DetectPlayer()
    {
        Collider2D closeHit = Physics2D.OverlapCircle(transform.position, closeDetectRadius, playerLayer);

        Vector2 forwardPos = (Vector2)transform.position + Vector2.right * direction * (visionRadius / 2);
        Collider2D visionHit = Physics2D.OverlapCircle(forwardPos, visionRadius, playerLayer);

        if (closeHit != null || visionHit != null)
        {
            player = (closeHit != null ? closeHit : visionHit).transform;
            currentState = State.Chase;
        }
    }

    bool CanSeePlayer()
    {
        Collider2D closeHit = Physics2D.OverlapCircle(transform.position, closeDetectRadius, playerLayer);

        Vector2 forwardPos = (Vector2)transform.position + Vector2.right * direction * (visionRadius / 2);
        Collider2D visionHit = Physics2D.OverlapCircle(forwardPos, visionRadius, playerLayer);

        return (closeHit != null || visionHit != null);
    }

    bool HasGroundAhead()
    {
        Vector2 origin = (Vector2)transform.position + new Vector2(direction * edgeOffset, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    bool HasGroundInDirection(int dir)
    {
        Vector2 origin = (Vector2)transform.position + new Vector2(dir * edgeOffset, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    void HandleFacing()
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }   

    public int GetDirection()
    {
        return direction;
    }
}