using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLostButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("Main Menu");
    }

}
