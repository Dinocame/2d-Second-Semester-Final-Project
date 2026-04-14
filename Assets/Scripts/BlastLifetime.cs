using UnityEngine;

public class BlastLifetime : MonoBehaviour
{
    public float lifetime = 0.25f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}