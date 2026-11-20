using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Game/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("General")]
    public int damage = 1;
    public int maxHp = 1;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chargeSpeed = 10f;
    public float chargeDistance = 6f;

    [Header("Vision")]
    public float visionRange = 6f;
    public float viewHalfAngle = 60f;
    public float heightTolerance = 1.2f;
    public float loseSightTime = 1.5f;

    [Header("Attack timing")]
    public float windupTime = 0.5f;
    public float cooldownTime = 1.5f;

    [Header("Projectile")]
    public float projectileSpeed = 6f;
    public float projectileLifetime = 5f;
        public float defeatDelay = 1f;
}