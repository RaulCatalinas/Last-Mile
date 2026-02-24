using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleController : MonoBehaviour
{
    [SerializeField, Range(3f, 15f)] private float minimumSpeed = 5f;
    [SerializeField, Range(3f, 15f)] private float maximumSpeed = 8f;

    private float speed;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minimumSpeed, maximumSpeed);
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = -speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoadEnd")) Destroy(gameObject);
    }
}
