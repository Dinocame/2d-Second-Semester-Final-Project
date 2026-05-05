using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial Scene");
    }
}
