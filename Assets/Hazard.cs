using UnityEngine;

public class Hazard : MonoBehaviour
{
    public EnemyStats stats;

    void OnTriggerStay2D(Collider2D other)
    {
        Health h = other.GetComponent<Health>();
        if (h != null)
            h.TakeDamage(stats.damage);
    }
}