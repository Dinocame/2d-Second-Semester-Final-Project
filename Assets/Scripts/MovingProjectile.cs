using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private Vector2 moveDirection;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }
}