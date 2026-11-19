using UnityEngine;

public class ChargerEnemy : MonoBehaviour
{
    enum State { Patrol, Windup, Charge, Recover }

    public EnemyStats stats;

    [Header("Zone (waypoints)")]
    public Transform leftPoint;
    public Transform rightPoint;

    State state = State.Patrol;
    Transform player;
    Transform target;
    SpriteRenderer sr;
    Color baseColor;
    float facing = 1f;
    float timer;
    float chargeDir;
    float chargeStartX;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
        target = rightPoint;
    }

    void Update()
    {
        switch (state)
        {
            case State.Patrol:
                MoveTo(target.position.x, stats.patrolSpeed);
                if (Mathf.Abs(transform.position.x - target.position.x) < 0.1f)
                    target = (target == rightPoint) ? leftPoint : rightPoint;

                if (CanSeePlayer() && IsOnScreen())
                {
                    state = State.Windup;
                    timer = stats.windupTime;
                    chargeDir = Mathf.Sign(player.position.x - transform.position.x);
                    facing = chargeDir;
                    sr.flipX = facing < 0f;
                }
                break;

            case State.Windup:
                sr.color = Color.Lerp(baseColor, Color.red, Mathf.PingPong(Time.time * 8f, 1f));
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    sr.color = baseColor;
                    chargeStartX = transform.position.x;
                    state = State.Charge;
                }
                break;

            case State.Charge:
                Vector3 p = transform.position;
                p.x += chargeDir * stats.chargeSpeed * Time.deltaTime;
                p.x = Mathf.Clamp(p.x, leftPoint.position.x, rightPoint.position.x);
                transform.position = p;

                bool atEdge = p.x <= leftPoint.position.x || p.x >= rightPoint.position.x;
                bool farEnough = Mathf.Abs(p.x - chargeStartX) >= stats.chargeDistance;
                if (atEdge || farEnough)
                {
                    sr.color = Color.gray;
                    state = State.Recover;
                    timer = stats.cooldownTime;
                }
                break;

            case State.Recover:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    sr.color = baseColor;
                    target = NearestPoint();
                    state = State.Patrol;
                }
                break;
        }
    }

    void MoveTo(float x, float speed)
    {
        float dx = x - transform.position.x;
        if (Mathf.Abs(dx) > 0.05f) facing = Mathf.Sign(dx);
        sr.flipX = facing < 0f;

        Vector3 p = transform.position;
        p.x = Mathf.MoveTowards(p.x, x, speed * Time.deltaTime);
        transform.position = p;
    }

    Transform NearestPoint()
    {
        float dl = Mathf.Abs(transform.position.x - leftPoint.position.x);
        float dr = Mathf.Abs(transform.position.x - rightPoint.position.x);
        return dl < dr ? leftPoint : rightPoint;
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

        if (Mathf.Abs(toPlayer.x) > stats.visionRange) return false;
        if (Mathf.Abs(toPlayer.y) > stats.heightTolerance) return false;
        if (Mathf.Sign(toPlayer.x) != facing) return false;

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
        if (leftPoint == null || rightPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(leftPoint.position, rightPoint.position);
    }
}