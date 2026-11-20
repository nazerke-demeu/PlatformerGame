using System.IO;
using UnityEngine;

public static class SaveSystem
{
    static SaveData data;

    static string Path
    {
        get { return System.IO.Path.Combine(Application.persistentDataPath, "save.json"); }
    }

    public static SaveData Data
    {
        get
        {
            if (data == null) Load();
            return data;
        }
    }

    public static void Load()
    {
        try
        {
            if (File.Exists(Path))
            {
                string json = File.ReadAllText(Path);
                data = JsonUtility.FromJson<SaveData>(json);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Save file is broken, using defaults: " + e.Message);
            data = null;
        }

        if (data == null || data.unlocked == null || data.unlocked.Count == 0)
            data = new SaveData();

        data.musicVol = Mathf.Clamp01(data.musicVol);
        if (data.bestScore < 0) data.bestScore = 0;
        if (!data.unlocked.Contains(1)) data.unlocked.Add(1);
    }

    public static void Save()
    {
        try
        {
            File.WriteAllText(Path, JsonUtility.ToJson(Data, true));
            Debug.Log("Saved to " + Path);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Could not save: " + e.Message);
        }
    }

    public static bool IsUnlocked(int levelNumber)
    {
        return Data.unlocked.Contains(levelNumber);
    }

    public static void Unlock(int levelNumber)
    {
        if (!Data.unlocked.Contains(levelNumber))
            Data.unlocked.Add(levelNumber);
    }

    public static void ResetAll()
    {
        data = new SaveData();
        Save();
    }
}