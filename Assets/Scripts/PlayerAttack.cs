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

    public GameObject movingProjectilePrefab;
    public float rightClickCooldown = 2f;

    private float nextRightClickTime = 0f;

    private PlayerDeath playerDeath;
    public float fireballSoulCost = 10f;
    void Start()
    {
        playerDeath = GetComponent<PlayerDeath>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + cooldown;
        }
        if (Input.GetMouseButtonDown(1) && Time.time >= nextRightClickTime)
        {
            if (playerDeath.soulPower >= fireballSoulCost)
            {
                playerDeath.soulPower -= fireballSoulCost;

                ShootMovingProjectile();

                nextRightClickTime = Time.time + rightClickCooldown;
            }
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;
        //Change position and offset
        Vector2 spawnPosition = (Vector2)transform.position + direction * fireDistance*2;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject currentAttack = Instantiate(blastPrefab, spawnPosition, rotation);

        Kill temp = currentAttack.GetComponent<Kill>();
        temp.owner = Kill.OwnerType.Player;
        //currentAttack.transform.localScale = new Vector3(4.0f * direction, 0.5f, 1f);
    }
    void ShootMovingProjectile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        Vector2 spawnPosition = (Vector2)transform.position + direction * fireDistance;

        GameObject projectile = Instantiate(movingProjectilePrefab, spawnPosition, Quaternion.identity);

        MovingProjectile proj = projectile.GetComponent<MovingProjectile>();
        proj.SetDirection(direction);

        Kill temp = projectile.GetComponent<Kill>();
        temp.owner = Kill.OwnerType.Player;
    }
}