using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public int coinsRequired = 3;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0.4f);
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

        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            Debug.Log("Level complete! (no next scene yet)");
    }

    void Update()
    {
        Collector c = FindFirstObjectByType<Collector>();
        if (c != null && c.coins >= coinsRequired)
            sr.color = Color.white;
    }
}