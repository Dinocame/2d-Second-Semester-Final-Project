using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ReincarnateOrSmthIdkLol : MonoBehaviour
{
    public GameObject ghost;
    private CinemachineVirtualCamera _cinemachine;

    void Start()
    { 
        _cinemachine = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Corpse"))
        {
            Reincarnate();
        }
    }

    void Reincarnate()
    {
        GameObject currentGhost = Instantiate(ghost, transform.position, Quaternion.identity);
        // Set cinemachine target to ghost instead of player
        _cinemachine.Follow = currentGhost.transform;
        _cinemachine.LookAt = currentGhost.transform;
        Destroy(gameObject);
    }
}
