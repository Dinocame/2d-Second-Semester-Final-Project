using UnityEngine;

public class DestroyTilemapWhenObjectDies : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        if (targetObject == null)
        {
            Destroy(gameObject);
        }
    }
}