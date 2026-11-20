using TMPro;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public LevelData[] levels;
    public Transform player;
    public TMP_Text bossHpText;

    void Awake()
    {
        GameSession.levelCount = levels.Length;
        int index = Mathf.Clamp(GameSession.currentLevel, 0, levels.Length - 1);
        Build(levels[index]);
    }

    void Build(LevelData level)
    {
                GameSession.timeLimit = level.timeLimit;
        player.position = level.playerStart;
        Camera.main.backgroundColor = level.backgroundColor;

        bool hasBoss = false;

        foreach (SpawnItem item in level.items)
        {
            if (item.prefab == null) continue;

            GameObject obj = Instantiate(item.prefab, item.position, Quaternion.identity, transform);

            if (item.width > 0f)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null && sr.drawMode == SpriteDrawMode.Tiled)
                    sr.size = new Vector2(item.width, sr.size.y);

                BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
                if (col != null)
                    col.size = new Vector2(item.width, col.size.y);
            }

            PatrollerEnemy patroller = obj.GetComponent<PatrollerEnemy>();
            if (patroller != null)
            {
                patroller.leftPoint = MakePoint(item.position.x - item.patrolRange, item.position.y);
                patroller.rightPoint = MakePoint(item.position.x + item.patrolRange, item.position.y);
            }

            ChargerEnemy charger = obj.GetComponent<ChargerEnemy>();
            if (charger != null)
            {
                charger.leftPoint = MakePoint(item.position.x - item.patrolRange, item.position.y);
                charger.rightPoint = MakePoint(item.position.x + item.patrolRange, item.position.y);
            }

            LevelExit exit = obj.GetComponent<LevelExit>();
            if (exit != null)
                exit.coinsRequired = level.coinsRequired;

            BossHealth boss = obj.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.hpText = bossHpText;
                hasBoss = true;
            }
        }

        if (bossHpText != null)
            bossHpText.gameObject.SetActive(hasBoss);
    }

    Transform MakePoint(float x, float y)
    {
        GameObject p = new GameObject("WaypointPoint");
        p.transform.position = new Vector3(x, y, 0f);
        p.transform.SetParent(transform);
        return p.transform;
    }
}