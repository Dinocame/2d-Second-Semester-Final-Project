using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public GameObject player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Corpse"))
        {
            player.GetComponent<PlayerDeath>().soulPower += collision.gameObject.GetComponent<SoulValue>().soulValue;
            Destroy(collision.gameObject);
        }
    }
}
