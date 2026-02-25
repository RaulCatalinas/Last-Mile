using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleController : MonoBehaviour
{

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = -ObstacleSpawner.Instance.GetRandomSpeed();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoadEnd")) Destroy(gameObject);
    }
}
