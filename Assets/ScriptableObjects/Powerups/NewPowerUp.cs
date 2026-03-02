using UnityEngine;

public enum PowerUpType
{
    SpeedBoost,
    ScoreMultiplier,
    ExtraLife,
    Invincibility,
    SlowDown,
    ScoreReduction,
    LoseLife,
    InvertedControls
}

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUps/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    [Header("General")]
    public string powerUpName;
    public string esPowerUpName;
    [TextArea] public string description;
    [TextArea] public string esDescription;
    public Sprite icon;
    public bool isTroll;

    [Header("Effect Settings")]
    public PowerUpType type;
    public float duration;
    public float value;

    [Header("Spawn Settings")]
    [Range(0f, 1f)]
    public float spawnChance = 0.1f;
}
