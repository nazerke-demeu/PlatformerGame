using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public EnemyStats stats;
    public TMP_Text hpText;
    public float winDelay = 1f;

    int hp;

    void Start()
    {
        hp = stats.maxHp;
        UpdateText();
    }

    public void TakeHit(int amount)
    {
        hp -= amount;
        UpdateText();

        if (hp <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<BossAttack>().enabled = false;
            Invoke(nameof(Win), winDelay);
        }
    }

       void Win()
    {
        GameSession.CompleteLevel();
    }

    void UpdateText()
    {
        hpText.text = "BOSS HP: " + Mathf.Max(hp, 0);
    }
}