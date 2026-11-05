using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float interval = 2f;
    public float fireballSpeed = 6f;

    float timer;
    Transform player;
    SpriteRenderer sr;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        timer = interval;
    }

    void Update()
    {
        sr.flipX = player.position.x > transform.position.x;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = interval;
        }
    }

    void Shoot()
    {
        GameObject f = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Vector2 dir = (player.position - transform.position).normalized;
        f.GetComponent<Rigidbody2D>().linearVelocity = dir * fireballSpeed;
    }
}