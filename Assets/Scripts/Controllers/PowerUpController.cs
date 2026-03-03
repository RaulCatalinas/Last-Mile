using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PowerUpController : MonoBehaviour
{
    private PowerUpData powerUpData;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform spawnPoint;
    private PowerUpSpawner spawner;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator FlashWarning()
    {
        float flashDuration = 0.15f;
        int flashCount = (int)(powerUpData.warningTime / (flashDuration * 2));

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }
    }

    IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(powerUpData.lifetime - powerUpData.warningTime);
        StartCoroutine(FlashWarning());

        yield return new WaitForSeconds(powerUpData.warningTime);
        Destroy(gameObject);
    }

    public void Initialize(PowerUpData data, Transform point, PowerUpSpawner owner)
    {
        spawnPoint = point;
        spawner = owner;
        powerUpData = data;
        spriteRenderer.sprite = powerUpData.icon;

        StartCoroutine(LifetimeRoutine());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        PowerUpsManager.Instance.ActivatePowerUp(powerUpData);
        AudioManager.Instance.PlayPowerUp(powerUpData.isTroll);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        spawner.FreeSpawnPoint(spawnPoint);
    }
}
