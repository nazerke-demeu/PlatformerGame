using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [HideInInspector] public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        BossHealth boss = other.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeHit(damage);
            Destroy(gameObject);
        }
    }
}