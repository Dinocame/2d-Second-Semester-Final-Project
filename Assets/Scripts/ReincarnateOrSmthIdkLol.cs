using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ReincarnateOrSmthIdkLol : MonoBehaviour
{
    public GameObject player;
    private CinemachineVirtualCamera _cinemachine;
    public float soulPower = 0f;

    void Start()
    { 
        _cinemachine = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        soulPower -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Corpse"))
        {
            Destroy(collision.gameObject);
            Reincarnate();
        }
    }

    void Reincarnate()
    {
        GameObject currentPlayer = Instantiate(player, transform.position, Quaternion.identity);
        // Set cinemachine target to ghost instead of player
        _cinemachine.Follow = currentPlayer.transform;
        _cinemachine.LookAt = currentPlayer.transform;
        PlayerDeath temp = currentPlayer.GetComponent<PlayerDeath>();
        temp.soulPower = soulPower;
        temp.isDead = false;
        Destroy(gameObject);
    }
}
