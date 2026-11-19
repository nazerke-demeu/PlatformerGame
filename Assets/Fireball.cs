using UnityEngine;

public class Fireball : MonoBehaviour
{
    int damage;
    float lifetime;

    public void Init(int dmg, float life)
    {
        damage = dmg;
        lifetime = life;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Health h = other.GetComponent<Health>();
        if (h != null)
        {
            h.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}