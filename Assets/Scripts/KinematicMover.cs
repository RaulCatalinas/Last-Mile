using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KinematicMover : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed = 2f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = -parallaxSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoadEnd")) Destroy(gameObject);
    }
}
