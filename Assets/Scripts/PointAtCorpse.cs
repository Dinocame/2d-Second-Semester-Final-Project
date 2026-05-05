using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCorpse : MonoBehaviour
{
    public GameObject[] corpseList;
    private Transform Ghost;
    private Transform closestCorpse;
    private GameObject arrow;

    void Start()
    {
        Ghost = transform.root;
        arrow = transform.GetChild(0).gameObject;
        corpseList = GameObject.FindGameObjectsWithTag("Corpse");
    }
    
    // Update is called once per frame
    void Update()
    {
        // if no corpse, hide arrow
        if (corpseList.Length == 0)
        {
            arrow.SetActive(false);
        }

        // else find closest
        else
        {
            arrow.SetActive(true);
            float minDist = Mathf.Infinity;
            foreach (GameObject corpse in corpseList)
            {
                float currDist = Vector3.Distance(corpse.transform.position, Ghost.position);
                if (currDist < minDist)
                {
                    minDist = currDist;
                    closestCorpse = corpse.transform;
                }
            }
            Vector3 dir = closestCorpse.position - Ghost.position;
            transform.right = dir;
        }
        
        // Point at closest
        
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