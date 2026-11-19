using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public PlayerStats stats;
    public GameObject bulletPrefab;

    Rigidbody2D rb;
    float dir = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
            dir = Mathf.Sign(rb.linearVelocity.x);
    }

    void OnFire()
    {
        if (Time.timeScale == 0f) return;

        if (Sfx.I != null) Sfx.I.Play(Sfx.I.shoot);
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(dir * stats.bulletSpeed, 0f);
        Destroy(b, stats.bulletLifetime);
    }
}