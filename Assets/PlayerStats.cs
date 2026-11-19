using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    [Header("Health")]
    public int maxHp = 3;
    public int blinkCount = 6;
    public float blinkInterval = 0.1f;
    public float fallLimit = -10f;

    [Header("Shooting")]
    public float bulletSpeed = 15f;
    public float bulletLifetime = 2f;
}