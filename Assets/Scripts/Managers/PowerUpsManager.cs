using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField] private PowerUpData scoreMultiplierPowerUp;

    public static PowerUpsManager Instance { get; private set; }

    private Dictionary<PowerUpType, Action<PowerUpData>> powerUpEffects;

    void Awake()
    {
        if (Instance == null) Instance = this;

        powerUpEffects = new Dictionary<PowerUpType, Action<PowerUpData>>
        {
            { PowerUpType.ExtraLife, ApplyExtraLife },
            { PowerUpType.SpeedBoost, p => StartCoroutine(SpeedBoostEffect(p)) },
            { PowerUpType.SlowDown, p => StartCoroutine(SlowDownEffect(p)) },
            { PowerUpType.ScoreMultiplier, p => StartCoroutine(ScoreMultiplierEffect(p)) },
            { PowerUpType.ScoreReduction, p => StartCoroutine(ScoreReductionEffect(p)) },
            { PowerUpType.Invincibility, p => StartCoroutine(InvincibilityEffect(p)) },
            { PowerUpType.LoseLife, _ => LoseLifeEffect() },
            { PowerUpType.InvertedControls, p => StartCoroutine(InvertedControlsEffect(p)) }
        };
    }

    public void ActivatePowerUp(PowerUpData powerUp)
    {
        if (powerUpEffects.TryGetValue(powerUp.type, out var effect)) effect(powerUp);
        else Debug.LogWarning($"No effect found for PowerUpType: {powerUp.type}");
    }

    void ApplyExtraLife(PowerUpData powerUp)
    {
        if (GameManager.playerLives == GameManager.selectedPlayer.lives)
        {
            ActivatePowerUp(scoreMultiplierPowerUp);

            return;
        }

        GameManager.Instance.GainLife();
        UIManager.Instance.UpdateLives(GameManager.playerLives);
    }

    void LoseLifeEffect()
    {
        if (GameManager.playerLives == 0)
        {
            GameManager.Instance.GameOver();

            return;
        }

        GameManager.Instance.LoseLife();
        UIManager.Instance.UpdateLives(GameManager.playerLives);
    }

    IEnumerator SpeedBoostEffect(PowerUpData powerUp)
    {
        PlayerController.Instance.SetSpeedMultiplier(1f + powerUp.value);
        yield return new WaitForSeconds(powerUp.duration);

        PlayerController.Instance.SetSpeedMultiplier(1f);

        if (powerUp.trollReward != null) ActivatePowerUp(powerUp.trollReward);
    }

    IEnumerator SlowDownEffect(PowerUpData powerUp)
    {
        PlayerController.Instance.SetSpeedMultiplier(1f - powerUp.value);
        yield return new WaitForSeconds(powerUp.duration);

        PlayerController.Instance.SetSpeedMultiplier(1f);

        if (powerUp.trollReward != null) ActivatePowerUp(powerUp.trollReward);
    }

    IEnumerator ScoreMultiplierEffect(PowerUpData powerUp)
    {
        ScoreManager.Instance.AddMultiplier(powerUp.value);
        yield return new WaitForSeconds(powerUp.duration);

        ScoreManager.Instance.RemoveMultiplier();

        if (powerUp.trollReward != null) ActivatePowerUp(powerUp.trollReward);
    }

    IEnumerator ScoreReductionEffect(PowerUpData powerUp)
    {
        ScoreManager.Instance.SetTemporaryMultiplier(powerUp.value);
        yield return new WaitForSeconds(powerUp.duration);

        ScoreManager.Instance.RestoreMultiplier();

        if (powerUp.trollReward != null) ActivatePowerUp(powerUp.trollReward);
    }

    IEnumerator InvincibilityEffect(PowerUpData powerUp)
    {
        PlayerController.Instance.SetInvincible(true);
        PlayerController.Instance.GetSpriteRenderer().color = Color.yellow;
        yield return new WaitForSeconds(powerUp.duration);

        PlayerController.Instance.GetSpriteRenderer().color = Color.white;
        PlayerController.Instance.SetInvincible(false);

        if (powerUp.trollReward != null) ActivatePowerUp(powerUp.trollReward);
    }

    IEnumerator InvertedControlsEffect(PowerUpData powerUp)
    {
        PlayerController.Instance.SetInvertedControls(true);
        yield return new WaitForSeconds(powerUp.duration);

        PlayerController.Instance.SetInvertedControls(false);

        if (powerUp.trollReward != null) ActivatePowerUp(powerUp.trollReward);
    }
}
