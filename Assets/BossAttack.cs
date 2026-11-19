using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public EnemyStats stats;
    public GameObject fireballPrefab;

    float timer;
    Transform player;
    SpriteRenderer sr;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        timer = stats.cooldownTime;
    }

    void Update()
    {
        sr.flipX = player.position.x > transform.position.x;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = stats.cooldownTime;
        }
    }

    void Shoot()
    {
        GameObject f = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        f.GetComponent<Fireball>().Init(stats.damage, stats.projectileLifetime);
        Vector2 dir = ((Vector2)player.position - (Vector2)transform.position).normalized;
        f.GetComponent<Rigidbody2D>().linearVelocity = dir * stats.projectileSpeed;
    }
}