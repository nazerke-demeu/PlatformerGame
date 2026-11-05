using UnityEngine;

public class PlatformRider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player") && c.transform.position.y > transform.position.y)
            c.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player"))
            c.transform.SetParent(null);
    }
}