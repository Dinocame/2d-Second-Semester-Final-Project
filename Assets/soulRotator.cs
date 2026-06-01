using UnityEngine;

public class RotateFourSquares : MonoBehaviour
{
    public Transform square1;
    public Transform square2;
    public Transform square3;
    public Transform square4;

    public float speed1 = 50f;
    public float speed2 = 100f;
    public float speed3 = 150f;
    public float speed4 = 200f;

    void Update()
    {
        // Rotate each square around the Z-axis (standard for 2D) at its designated speed
        if (square1 != null) square1.Rotate(Vector3.forward, speed1 * Time.deltaTime);
        if (square2 != null) square2.Rotate(Vector3.forward, speed2 * Time.deltaTime);
        if (square3 != null) square3.Rotate(Vector3.forward, speed3 * Time.deltaTime);
        if (square4 != null) square4.Rotate(Vector3.forward, speed4 * Time.deltaTime);
    }
}
