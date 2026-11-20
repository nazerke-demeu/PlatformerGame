using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 offset = new Vector2(2f, 1f);
    public float smooth = 6f;

    void Start()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player").transform;

        transform.position = Goal();
    }

    void LateUpdate()
    {
        if (target == null) return;
        transform.position = Vector3.Lerp(transform.position, Goal(), smooth * Time.deltaTime);
    }

    Vector3 Goal()
    {
        return new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
    }
}