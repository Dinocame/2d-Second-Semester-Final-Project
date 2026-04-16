using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public bool isDead = false;
    public GameObject ghost;
    // private Cinemachine

    void Start()
    {
        // cinemachine = GetComponent Cinemachine
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
        // Cinemachine target = currentGhost
        Destroy(gameObject);
    }
}
