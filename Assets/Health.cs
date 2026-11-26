using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public PlayerStats stats;
    public int hp;

    bool invincible;
    bool dead;
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
        if (invincible || dead) return;

        hp -= amount;
        if (hp <= 0)
        {
            Die();
            return;
        }

        if (Sfx.I != null) Sfx.I.PlayHit();
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
        if (dead) return;
        dead = true;
        StopAllCoroutines();

        float delay = 0f;
        if (Sfx.I != null)
        {
            Sfx.I.PlayFixed(Sfx.I.death);
            delay = Sfx.I.deathDelay;
        }

        GetComponent<PlayerController>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        sr.enabled = false;

        Invoke(nameof(Reload), delay);
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}