using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ReincarnateOrSmthIdkLol : MonoBehaviour
{
    public GameObject player;
    private CinemachineVirtualCamera _cinemachine;

    void Start()
    { 
        _cinemachine = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
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
        Destroy(gameObject);
    }
}
