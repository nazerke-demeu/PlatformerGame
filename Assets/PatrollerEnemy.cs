using UnityEngine;

public class PatrollerEnemy : MonoBehaviour
{
    enum State { Patrol, Alert, Chase, Return }

    [Header("Waypoints")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Vision")]
    public float visionRange = 6f;
    public float viewHalfAngle = 60f;
    public float loseSightTime = 1.5f;

    [Header("Alert (telegraph before chase)")]
    public float alertTime = 0.5f;

    State state = State.Patrol;
    Transform player;
    Transform target;
    SpriteRenderer sr;
    Color baseColor;
    float facing = 1f;
    float timer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
        target = rightPoint;
    }

    void Update()
    {
        bool sees = CanSeePlayer();

        switch (state)
        {
            case State.Patrol:
                MoveTo(target.position.x, patrolSpeed);
                if (Mathf.Abs(transform.position.x - target.position.x) < 0.1f)
                    target = (target == rightPoint) ? leftPoint : rightPoint;
                if (sees) StartAlert();
                break;

            case State.Alert:
                sr.color = Color.red;
                Face(player.position.x);
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    sr.color = baseColor;
                    state = State.Chase;
                    timer = loseSightTime;
                }
                break;

            case State.Chase:
                MoveTo(player.position.x, chaseSpeed);
                if (sees) timer = loseSightTime;
                else timer -= Time.deltaTime;
                if (timer <= 0f) state = State.Return;
                break;

            case State.Return:
                target = NearestPoint();
                MoveTo(target.position.x, patrolSpeed);
                if (sees) StartAlert();
                else if (Mathf.Abs(transform.position.x - target.position.x) < 0.1f)
                    state = State.Patrol;
                break;
        }
    }

    void StartAlert()
    {
        state = State.Alert;
        timer = alertTime;
    }

    void Face(float x)
    {
        float dx = x - transform.position.x;
        if (Mathf.Abs(dx) > 0.05f) facing = Mathf.Sign(dx);
        sr.flipX = facing < 0f;
    }

    void MoveTo(float x, float speed)
    {
        Face(x);
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

    bool CanSeePlayer()
    {
        Vector2 origin = transform.position;
        Vector2 toPlayer = (Vector2)player.position - origin;

        if (toPlayer.magnitude > visionRange) return false;
        if (Vector2.Angle(new Vector2(facing, 0f), toPlayer) > viewHalfAngle) return false;

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}