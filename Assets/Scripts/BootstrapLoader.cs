using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapLoader : MonoBehaviour
{
    // Title screen
    public string titleSceneName = "TitleScreen";

    void Start()
    {
        // Immediately load the title scene
        SceneManager.LoadScene(titleSceneName);
    }
}
