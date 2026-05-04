using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCorpse : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Target()
    {
        transform.LookAt(target);
    }

}

    /*
    public Transform Corpse;
    public Transform Player;
    public float percentWayToCorpse;
    
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;     
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Corpse.position - Player.position;
        transform.right = dir;
        CalculatePosition(dir);
    }

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
            float y = Mathf.Tan(transform.eulerAngles.z/180f*2*Mathf.PI) * distToEdgeX;
            transform.position = Player.position + percentWayToCorpse * y * Vector3.up + percentWayToCorpse * distToEdgeX * Vector3.right;
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