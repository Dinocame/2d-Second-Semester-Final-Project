using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public int soulPower = 15;

    public GameObject corpsePrefab; // 👈 assign in Inspector

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Spawn corpse at enemy position
        GameObject currentCorpse = Instantiate(corpsePrefab, transform.position, Quaternion.identity);
        currentCorpse.GetComponent<SoulValue>().soulValue = soulPower;

        Destroy(gameObject);
    }
}