using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;

    private Rigidbody2D rb;
    private PlayerStats stats;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GameManager.selectedPlayer;
        GetComponent<SpriteRenderer>().sprite = stats.sprite;
    }

    void FixedUpdate()
    {
        rb.linearVelocityY = stats.lateralSpeed * joystick.Vertical;
    }
}
