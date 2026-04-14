using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject blastPrefab;
    public Transform firePoint;

    public float fireDistance = 1.0f; // how far from player

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Direction from player to mouse
        Vector2 direction = (mousePos - transform.position).normalized;

        // Move fire point outward from player
        Vector2 spawnPosition = (Vector2)transform.position + direction * fireDistance;

        // Calculate rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        Instantiate(blastPrefab, spawnPosition, rotation);
    }
}