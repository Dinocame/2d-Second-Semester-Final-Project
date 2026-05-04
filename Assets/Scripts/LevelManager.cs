using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject AcceptYourFate;
    public GameObject Player;

    public GameObject Corpse;
    
    // Start is called before the first frame update
    void Start()
    {
        AcceptYourFate = GameObject.Find("AcceptYourFate");
        AcceptYourFate.SetActive(false);
    }
}
