using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InstructionButton : MonoBehaviour
{
    public void LoadInstructions()
    {
        Debug.Log("Loading Instructions");
        SceneManager.LoadScene("Instructions");
    }
}
