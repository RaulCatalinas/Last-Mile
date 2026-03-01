using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Configs/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Visual")]
    public Sprite sprite;

    [Header("Life")]
    [Min(1)] public int lives = 3;
    [Min(0f)] public float invincibilityTime = 1.5f;

    [Header("Score")]
    [Min(1f)] public float scoreMultiplier = 1f;

    [Header("Movement")]
    [Range(1f, 10f)] public float lateralSpeed = 5f;
    [Range(0.1f, 2f)] public float acceleration = 0.5f;
}