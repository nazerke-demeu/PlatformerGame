using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    enum State { Idle, Aim, Cooldown }

    public EnemyStats stats;
    public GameObject projectilePrefab;

    State state = State.Idle;
    float timer;
    Transform player;
    SpriteRenderer sr;
    Color baseColor;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (CanSeePlayer() && IsOnScreen())
                {
                                        state = State.Aim;
                    timer = stats.windupTime;
                    if (Sfx.I != null) Sfx.I.PlayFixed(Sfx.I.enemyWindup);
                }
                break;

            case State.Aim:
                sr.flipX = player.position.x < transform.position.x;
                sr.color = Color.Lerp(baseColor, Color.red, Mathf.PingPong(Time.time * 8f, 1f));
                timer -= Time.deltaTime;

                if (!CanSeePlayer())
                {
                    sr.color = baseColor;
                    state = State.Idle;
                }
                else if (timer <= 0f)
                {
                    Fire();
                    sr.color = baseColor;
                    state = State.Cooldown;
                    timer = stats.cooldownTime;
                }
                break;

            case State.Cooldown:
                timer -= Time.deltaTime;
                if (timer <= 0f) state = State.Idle;
                break;
        }
    }

    void Fire()
    {
        GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        p.GetComponent<Fireball>().Init(stats.damage, stats.projectileLifetime);
        Vector2 dir = ((Vector2)player.position - (Vector2)transform.position).normalized;
        p.GetComponent<Rigidbody2D>().linearVelocity = dir * stats.projectileSpeed;
        if (Sfx.I != null) Sfx.I.Play(Sfx.I.shoot);
    }

    bool IsOnScreen()
    {
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position);
        return v.x > 0f && v.x < 1f && v.y > 0f && v.y < 1f;
    }

    bool CanSeePlayer()
    {
        Vector2 origin = transform.position;
        Vector2 toPlayer = (Vector2)player.position - origin;
        if (toPlayer.magnitude > stats.visionRange) return false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, toPlayer.normalized, toPlayer.magnitude);
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider.gameObject == gameObject) continue;
            if (h.collider.isTrigger) continue;
            return h.collider.CompareTag("Player");
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (stats == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.visionRange);
    }
}