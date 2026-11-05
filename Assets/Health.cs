using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHp = 3;
    public int hp;
    public float fallLimit = -10f;

    bool invincible;
    SpriteRenderer sr;

    void Awake()
    {
        hp = maxHp;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (transform.position.y < fallLimit)
            Die();
    }

    public void TakeDamage(int amount)
    {
        if (invincible) return;

        hp -= amount;
                Sfx.I.Play(Sfx.I.hurt);
        if (hp <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(Invincibility());
    }

    public void Heal(int amount)
    {
        hp = Mathf.Min(hp + amount, maxHp);
    }

    IEnumerator Invincibility()
    {
        invincible = true;
        for (int i = 0; i < 6; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invincible = false;
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}