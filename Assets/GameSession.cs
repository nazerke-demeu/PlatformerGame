using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSession
{
    public static int currentLevel = 0;
    public static int levelCount = 3;

    public static void StartLevel(int index)
    {
        currentLevel = index;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public static void CompleteLevel()
    {
        int next = currentLevel + 1;
        if (next < levelCount)
        {
            StartLevel(next);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Win");
        }
    }
}