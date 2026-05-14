using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 1.5f;

    private Vector2 moveDirection;

    private SpriteRenderer sr;

    private float timer = 0f;

    public float fadeInPercent = 0.1f;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Color color = sr.color;
        color.a = 0.1f;
        sr.color = color;
    }

    void Update()
    {
        timer += Time.deltaTime;

        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

        float lifePercent = timer / lifetime;

        float alpha;

        if (lifePercent < fadeInPercent)
        {
            alpha = Mathf.Lerp(0.1f, 1f, lifePercent / fadeInPercent);
        }
        else
        {
            alpha = Mathf.Lerp(1f, 0f,(lifePercent - fadeInPercent) / (1f - fadeInPercent));
        }

        Color color = sr.color;
        color.a = alpha;
        sr.color = color;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}