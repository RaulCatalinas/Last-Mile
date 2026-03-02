using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;

    private Rigidbody2D rb;
    private PlayerStats stats;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GameManager.selectedPlayer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stats.sprite;
    }

    void FixedUpdate()
    {
        rb.linearVelocityY = stats.lateralSpeed * joystick.Vertical;
    }

    IEnumerator FlashRed()
    {
        isInvincible = true;
        float flashDuration = 0.1f;
        int flashCount = (int)(stats.invincibilityTime / (flashDuration * 2));

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }

        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Obstacle")) return;
        if (isInvincible) return;

        if (GameManager.isGameOver)
        {
            GameManager.Instance.GameOver();

            return;
        }

        StartCoroutine(FlashRed());
        GameManager.Instance.LoseLife();
    }
}
