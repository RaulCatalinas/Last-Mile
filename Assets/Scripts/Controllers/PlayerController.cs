using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;

    private Rigidbody2D rb;
    private PlayerStats stats;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;
    private float speedMultiplier = 1f;
    private bool invertedControls = false;

    public static PlayerController Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        stats = GameManager.selectedPlayer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stats.sprite;
    }

    void Start()
    {
        UIManager.Instance.InitializeLives(stats.lives);
    }

    void FixedUpdate()
    {
        var vertical = invertedControls ? -joystick.Vertical : joystick.Vertical;
        rb.linearVelocityY = stats.lateralSpeed * speedMultiplier * vertical;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void SetInvertedControls(bool inverted)
    {
        invertedControls = inverted;
    }

    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
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
        if (isInvincible)
        {
            Destroy(collision.gameObject);
            return;
        }

        if (GameManager.isGameOver)
        {
            GameManager.Instance.GameOver();
            return;
        }

        StartCoroutine(FlashRed());
        GameManager.Instance.LoseLife();
        UIManager.Instance.UpdateLives(GameManager.playerLives);
    }
}
