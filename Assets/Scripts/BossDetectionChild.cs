using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetectionChild : MonoBehaviour
{
    [Header("Detection Settings")]
    public float timeDelay = 1f;
    public float detectionRadius = 5f;
    public LayerMask playerLayer;
    [Header("Boss stuff")] 
    public GameObject bossBarObj;
    private SoulBar soulBar;
    private EnemyHealth bossHealth;
    
    private void Start()
    {
        bossBarObj.SetActive(false);
        soulBar = bossBarObj.GetComponent<SoulBar>();
        bossHealth = GetComponent<EnemyHealth>();
        bossHealth.soulBar = soulBar;
        soulBar.SetSoulPowerMax(bossHealth.health);
        InvokeRepeating(nameof(CheckForPlayer), 0f, timeDelay);
    }

    private void CheckForPlayer()
    {
        //Debug.Log(gameObject.name + " is running CheckForPlayer()");
        // Cast a circle around this object
        Collider2D player = Physics2D.OverlapCircle(
            transform.position,
            detectionRadius,
            playerLayer
        );

        if (player != null)
        {
            //Debug.Log("Player detected!");
            bossBarObj.SetActive(true);
            soulBar.SetSoulPower(bossHealth.health);
        }
        else
        {
            //Debug.Log("Player not in range.");
        }
    }

    // Draw the detection radius in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
