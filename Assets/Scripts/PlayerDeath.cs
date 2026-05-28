using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;


public class PlayerDeath : MonoBehaviour
{
    public int health = 1;
    public bool isDead = false;
    public GameObject ghost;
    public float soulPower = 60f;
    public float soulPowerMax = 100f;
    public SoulBar soulBar;
    
    private CinemachineVirtualCamera _cinemachine;
    private TMP_Text soulPowerText;

    

    void Awake()
    {
        soulBar = GameObject.Find("Soul Bar").GetComponent<SoulBar>();
        soulPowerText =  GameObject.FindWithTag("soulpower").GetComponent<TMP_Text>();
        UpdateSoulText();
    }
    void Start()
    { 
        _cinemachine = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        
        //soulPowerText.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateSoulText();
        if (isDead)
        {
            PlayerDies();
        }
        
        if (soulPower > soulPowerMax)
        {
            soulPower = soulPowerMax;
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
        currentGhost.GetComponent<ReincarnateOrSmthIdkLol>().soulPowerMax = soulPowerMax;
        
        soulPowerText.gameObject.SetActive(true);

        Destroy(gameObject);
    }
    void UpdateSoulText()
    {
        soulPowerText.text = "Soul Power: " + Mathf.CeilToInt(soulPower) + "/" + Mathf.CeilToInt(soulPowerMax);
        soulBar.SetSoulPower(soulPower);
    }
}