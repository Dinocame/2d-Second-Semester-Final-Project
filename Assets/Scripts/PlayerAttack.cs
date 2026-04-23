using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject blastPrefab;

    public float fireDistance = 1.0f;

    public float cooldown = 0.5f;   // time between shots
    private float nextFireTime = 0f;

    public float attackDamage = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + cooldown;
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        Vector2 spawnPosition = (Vector2)transform.position + direction * fireDistance*3;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject currentAttack = Instantiate(blastPrefab, spawnPosition, rotation);
        Kill temp = currentAttack.GetComponent<Kill>();
        temp.owner = Kill.OwnerType.Player;
    }
}