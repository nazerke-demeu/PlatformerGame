using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public int coinsRequired = 3;

    SpriteRenderer sr;
    Collector collector;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        collector = FindFirstObjectByType<Collector>();
    }

    void Update()
    {
        bool open = collector != null && collector.coins >= coinsRequired;
        sr.color = open ? Color.white : new Color(1f, 1f, 1f, 0.4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Collector c = other.GetComponent<Collector>();
        if (c == null) return;

        if (c.coins < coinsRequired)
        {
            Debug.Log("Collect " + coinsRequired + " coins first!");
            return;
        }

        GameSession.CompleteLevel();
    }
}