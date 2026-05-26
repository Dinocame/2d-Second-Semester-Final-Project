using UnityEngine;

public class LeftRightMover : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 2f;

    private Vector3 startPosition;
    private int direction = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        float offset = transform.position.x - startPosition.x;

        if (offset >= distance)
            direction = -1;
        else if (offset <= -distance)
            direction = 1;
    }
}