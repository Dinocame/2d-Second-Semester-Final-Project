using System.Collections;
using System.Collections.Generic;
using Unity.Play.Publisher.Editor;
using UnityEngine;

public class BossDetectionChild : MonoBehaviour
{
    //public <Script Name For Boss Health Visuals> bh;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            Debug.Log("Player entered Boss");
            //bh.Enter(); <- create a Enter() public void method in the script for Boss Health visuals
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            Debug.Log("Player exited Boss");
            
            //bh.Left(); <- create a Left() public void method in the script for Boss Health visuals
    }

    
    
}
