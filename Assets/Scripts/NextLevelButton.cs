using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    private bool hasPressed = false;

    public void OnGripPressed()
    {
        if (hasPressed) return;
        hasPressed = true;

        Debug.Log("✔️ Grip button pressed. Loading next scene.");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("⚠️ No next scene found in Build Settings!");
        }
    }
}
