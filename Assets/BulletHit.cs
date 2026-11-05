using UnityEngine;

public class BulletHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        BossHealth boss = other.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeHit(1);
            Destroy(gameObject);
        }
    }
}