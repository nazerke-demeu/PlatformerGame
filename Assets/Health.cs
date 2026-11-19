using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public PlayerStats stats;
    public int hp;

    bool invincible;
    SpriteRenderer sr;

    void Awake()
    {
        hp = stats.maxHp;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (transform.position.y < stats.fallLimit)
            Die();
    }

    public void TakeDamage(int amount)
    {
        if (invincible) return;

        if (Sfx.I != null) Sfx.I.Play(Sfx.I.hurt);
        hp -= amount;
        if (hp <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(Invincibility());
    }

    public void Heal(int amount)
    {
        hp = Mathf.Min(hp + amount, stats.maxHp);
    }

    IEnumerator Invincibility()
    {
        invincible = true;
        for (int i = 0; i < stats.blinkCount; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(stats.blinkInterval);
            sr.enabled = true;
            yield return new WaitForSeconds(stats.blinkInterval);
        }
        invincible = false;
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}