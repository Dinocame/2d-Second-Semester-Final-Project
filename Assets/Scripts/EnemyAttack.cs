using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attackPrefab;

    public float attackRange = 2f;
    public float cooldown = 1.5f;

    public float windupTime = 0.3f;   // pause BEFORE attack
    public float recoveryTime = 0.5f; // pause AFTER attack

    private float nextAttackTime = 0f;
    private float stateTimer = 0f;

    private Transform player;
    private Rigidbody2D rb;
    private EnemyMovement movement;

    private enum AttackState { Idle, Windup, Recovery }
    private AttackState currentState = AttackState.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        FindPlayer();
        HandleState();
    }

    void HandleState()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case AttackState.Idle:

                if (distance <= attackRange && Time.time >= nextAttackTime)
                {
                    currentState = AttackState.Windup;
                    stateTimer = windupTime;

                    movement.LockMovement(windupTime + recoveryTime);
                }

                break;

            case AttackState.Windup:

                stateTimer -= Time.deltaTime;

                if (stateTimer <= 0)
                {
                    Attack();
                    currentState = AttackState.Recovery;
                    stateTimer = recoveryTime;
                }

                break;

            case AttackState.Recovery:

                stateTimer -= Time.deltaTime;

                if (stateTimer <= 0)
                {
                    currentState = AttackState.Idle;
                    nextAttackTime = Time.time + cooldown;
                }

                break;
        }
    }

    void FindPlayer()
    {
        if (player != null) return;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }
    void Attack()
    {
        movement.LockMovement(0.5f);
        float direction = transform.localScale.x > 0 ? 1 : -1;

        // Get enemy width
        Collider2D col = GetComponent<Collider2D>();
        float enemyHalfWidth = col.bounds.extents.x;

        float attackHalfWidth = 1.25f; // half of 2.5 scale

        float offset = enemyHalfWidth + attackHalfWidth + 0.1f;

        Vector2 spawnPos = (Vector2)transform.position + new Vector2(-direction * offset*1.5f, 0);

        GameObject attack = Instantiate(attackPrefab, spawnPos, Quaternion.identity);

        attack.transform.localScale = new Vector3(2.5f * direction, 0.5f, 1f);
    }
}