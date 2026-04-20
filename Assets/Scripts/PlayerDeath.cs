using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerDeath : MonoBehaviour
{
    public bool isDead = false;
    public GameObject ghost;
    private CinemachineVirtualCamera _cinemachine;
    private Camera mainCam;

    void Start()
    { 
        mainCam = Camera.main;
        _cinemachine = mainCam.gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            PlayerDies();
        }
    }

    void PlayerDies()
    {
        GameObject currentGhost = Instantiate(ghost, transform.position, Quaternion.identity);
        // Set cinemachine target to ghost instead of player
        _cinemachine.Follow = currentGhost.transform;
        _cinemachine.LookAt = currentGhost.transform;
        Destroy(gameObject);
    }
}