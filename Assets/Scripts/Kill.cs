using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public enum OwnerType { Player, Enemy }
    public OwnerType owner;

    public int damage = 1;

    private HashSet<GameObject> hitObjects = new HashSet<GameObject>();

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (hitObjects.Contains(collision.gameObject))
            return;

        hitObjects.Add(collision.gameObject);

        // PLAYER HIT 
        if (collision.CompareTag("Player") && owner == OwnerType.Enemy)
        {
            collision.GetComponent<PlayerDeath>()?.TakeDamage(damage);
        }

        //ENEMY HIT
        if (collision.CompareTag("Enemy") && owner == OwnerType.Player)
        {
            collision.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }

        //CORPSE 
        if (collision.CompareTag("Corpse") && owner == OwnerType.Player)
        {
            Destroy(collision.gameObject);

            PlayerDeath pd = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>();
            pd.soulPower += collision.GetComponent<SoulValue>().soulValue;
        }
    }
}
