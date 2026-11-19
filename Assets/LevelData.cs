using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnItem
{
    public GameObject prefab;
    public Vector2 position;
    public float width = 0f;
    public float patrolRange = 3f;
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName = "Level";
    public Vector2 playerStart = new Vector2(-8f, -2f);
    public Color backgroundColor = new Color(0.1f, 0.15f, 0.3f);
    public int coinsRequired = 3;
    public float timeLimit = 90f;
    public List<SpawnItem> items = new List<SpawnItem>();
}