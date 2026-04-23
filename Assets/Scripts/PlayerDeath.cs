using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerDeath : MonoBehaviour
{
    public int health = 1;

    public bool isDead = false;
    public GameObject ghost;
    private CinemachineVirtualCamera _cinemachine;
    public float soulPower = 60f;

    void Start()
    { 
        _cinemachine = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (isDead)
        {
            PlayerDies();
        }
    }

    // Take Damage
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            isDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            TakeDamage(health); //die                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        }
    }

    public void PlayerDies()
    {
        if (isDead == false) return; // safety check

        GameObject currentGhost = Instantiate(ghost, transform.position, Quaternion.identity);

        _cinemachine.Follow = currentGhost.transform;
        _cinemachine.LookAt = currentGhost.transform;

        currentGhost.GetComponent<ReincarnateOrSmthIdkLol>().soulPower = soulPower;

        Destroy(gameObject);
    }
}