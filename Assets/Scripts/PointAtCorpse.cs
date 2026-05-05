using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCorpse : MonoBehaviour
{
    public Transform Corpse;
    private Transform Player;
    public float distanceFromPlayer;

    void Start()
    {
        Player = transform.root;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Corpse.position - Player.position;
        transform.right = dir;
        //transform.position = Player.position + distanceFromPlayer * dir.normalized;
        
    }
}
/*
    public void CalculatePosition(Vector3 dir)
    {
        float distToPlayer = dir.magnitude;

        float verticalLimit = mainCamera.orthographicSize;
        float horizontalLimit = verticalLimit * mainCamera.aspect;

        float distToEdgeX = Mathf.Abs(horizontalLimit / dir.x);
        float distToEdgeY = Mathf.Abs(verticalLimit / dir.y);

        // If corpse is in scene
        if (dir.x < distToEdgeX && dir.y < distToEdgeY)
        {
            transform.position = Player.position + percentWayToCorpse * distToPlayer * dir.normalized;
        }



        // if corpse is off screen horizontally
        else if (dir.x > distToEdgeX && dir.y < distToEdgeY)
        {
            float y = Mathf.Tan(transform.eulerAngles.z / 180f * 2 * Mathf.PI) * distToEdgeX;
            transform.position = Player.position + percentWayToCorpse * y * Vector3.up +
                                 percentWayToCorpse * distToEdgeX * Vector3.right;
        }
        // if corpse is off screen vertically
        else if (dir.x > distToEdgeX && dir.y > distToEdgeY)
        {

        }
        // if out of corner
        else
        {

        }

        float finalDistance = Mathf.Min(distToEdgeX, distToEdgeY);

        Vector3 edgePosition = Camera.main.transform.position + (dir * finalDistance);
        */