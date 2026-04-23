using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseHarvesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1f, Vector2.zero, 1f, LayerMask.GetMask("Corpse"));
            if (hit.collider != null)
            {
                gameObject.GetComponent<PlayerDeath>().soulPower += hit.collider.gameObject.GetComponent<SoulValue>().soulValue;
                Destroy(hit.collider.gameObject);
            }
        }
    } 
}
