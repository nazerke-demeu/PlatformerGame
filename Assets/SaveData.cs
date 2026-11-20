using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int version = 1;
    public int bestScore = 0;
    public List<int> unlocked = new List<int> { 1 };
    public float musicVol = 0.8f;
}