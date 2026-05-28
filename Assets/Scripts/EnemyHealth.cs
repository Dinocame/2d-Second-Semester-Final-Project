using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public int soulPower = 15;
    public SoulBar soulBar;

    public GameObject corpsePrefab; // 👈 assign in Inspector

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        Debug.Log("Boss health at " + health);
        if (soulBar != null)
        {
            Debug.Log("SoulBar working and updating");
            soulBar.SetSoulPower(health);
        }

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