using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;  // <- Import TMP namespace

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [HideInInspector] public Box[] boxes;
    public TMP_Text levelCompleteText; // Use TMP_Text instead of Text
    public float displayTime = 1f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        boxes = FindObjectsOfType<Box>();
        levelCompleteText.gameObject.SetActive(false);
    }

    public void CheckLevelComplete()
    {
        foreach (Box box in boxes)
        {
            if (!box.IsOnGoal())
                return; // not complete
        }

        // All boxes on goals → level complete
        StartCoroutine(LevelCompleteCoroutine());
    }

    IEnumerator LevelCompleteCoroutine()
    {
        levelCompleteText.text = "Level Complete!";
        levelCompleteText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        // Load next level
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level 1")
            SceneManager.LoadScene("Level 2");
        else if (currentScene == "Level 2")
            SceneManager.LoadScene("Level 3");
        else
        {
            levelCompleteText.text = "Game Complete!";
            yield return new WaitForSeconds(displayTime);
        }
    }
}
