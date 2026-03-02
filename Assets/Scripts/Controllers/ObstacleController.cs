using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(AudioSource))]
public class ObstacleController : MonoBehaviour
{
    [SerializeField] private Transform smokeStartPoint;
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private bool useDoubleSpeed = false;

    private Rigidbody2D rb;
    private float speed;

    void Awake()
    {
        var initialSpeed = ObstacleSpawner.Instance.GetRandomSpeed();

        speed = useDoubleSpeed ? initialSpeed * 2f : initialSpeed;

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = -speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoadEnd")) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        AudioManager.Instance.PlayCrash(GameManager.isGameOver, speed);

        if (GameManager.isGameOver)
        {
            Instantiate(smokePrefab, smokeStartPoint.position, Quaternion.identity);

            return;
        }

        Destroy(gameObject);
    }
}
