using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSession
{
    public static int currentLevel = 0;
    public static int levelCount = 3;
    public static int runScore = 0;
        public static float timeLimit = 0f;

    public static void StartLevel(int index)
    {
        runScore = 0;
        LoadLevel(index);
    }

    static void LoadLevel(int index)
    {
        currentLevel = index;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public static void CompleteLevel()
    {
        Collector c = Object.FindFirstObjectByType<Collector>();
        if (c != null) runScore += c.coins;

        SaveData d = SaveSystem.Data;
        int nextNumber = currentLevel + 2;
        if (nextNumber <= levelCount)
            SaveSystem.Unlock(nextNumber);
        if (runScore > d.bestScore)
            d.bestScore = runScore;
        SaveSystem.Save();

        int next = currentLevel + 1;
        if (next < levelCount)
        {
            LoadLevel(next);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Win");
        }
    }
}