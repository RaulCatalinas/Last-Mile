using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float speed = 3f;
    [SerializeField, Range(0.1f, 2f)] private float acceleration = 0.5f;
    [SerializeField] private FloatingJoystick joystick;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        speed += acceleration * Time.fixedDeltaTime;
        rb.linearVelocityY = speed * joystick.Vertical;
    }
}
