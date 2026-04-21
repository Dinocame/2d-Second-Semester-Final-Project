using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadLevelOne()
    {
        Debug.Log("Loading level one");
        SceneManager.LoadScene("Level 1");
    }

}