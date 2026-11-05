using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float distance = 3f;
    public float speed = 2f;

    Vector3 start;
    SpriteRenderer sr;
    float prevX;

    void Start()
    {
        start = transform.position;
        sr = GetComponent<SpriteRenderer>();
        prevX = start.x;
    }

    void Update()
    {
        float x = start.x + Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        transform.position = new Vector3(x, start.y, start.z);

        sr.flipX = x < prevX;
        prevX = x;
    }
}