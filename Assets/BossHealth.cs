using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int maxHp = 10;
    public TMP_Text hpText;

    int hp;

    void Start()
    {
        hp = maxHp;
        UpdateText();
    }

    public void TakeHit(int amount)
    {
        hp -= amount;
        UpdateText();

        if (hp <= 0)
        {
            GetComponent<BossAttack>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke(nameof(Win), 1f);
        }
    }

    void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void UpdateText()
    {
        hpText.text = "BOSS HP: " + Mathf.Max(hp, 0);
    }
}