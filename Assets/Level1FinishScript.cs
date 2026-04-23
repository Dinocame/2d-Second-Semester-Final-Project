
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene switching

public class Level1FinishScript: MonoBehaviour
{
    // The name of the scene you want to load
    public string MainMenuScene;

    // This built-in Unity function runs when the object's collider is clicked
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(MainMenuScene);
        }
    }
}
