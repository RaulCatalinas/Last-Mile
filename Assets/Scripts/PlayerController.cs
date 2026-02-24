using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float speed = 3f;
    [SerializeField, Range(0.1f, 2f)] private float acceleration = 0.5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        speed += acceleration * Time.fixedDeltaTime;
        rb.linearVelocityY = -speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle")) GameManager.Instance.GameOver();
    }
}
