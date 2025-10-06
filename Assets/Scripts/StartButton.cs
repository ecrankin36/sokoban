using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // This function will be called when the button is pressed
    public void StartGame()
    {
		Debug.Log("Start button pressed!");
        SceneManager.LoadScene("Level 1"); // Level1
    }
}
