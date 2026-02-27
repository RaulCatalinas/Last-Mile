using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleController : MonoBehaviour
{
    [SerializeField] private Transform smokeStartPoint;
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private bool useDoubleSpeed = false;

    private Rigidbody2D rb;
    private float speed;
    private AudioSource crashAudioSource;
    //private ParticleSystem smokeParticleSystem;

    void Awake()
    {
        speed = ObstacleSpawner.Instance.GetRandomSpeed();
        rb = GetComponent<Rigidbody2D>();
        crashAudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = useDoubleSpeed ? -(speed * 2f) : -speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoadEnd")) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ScoreManager.Instance.SaveMaxScore();
            crashAudioSource.Play();
            Instantiate(smokePrefab, smokeStartPoint.position, Quaternion.identity);
            GameManager.Instance.GameOver();
            UIManager.Instance.ShowGameOverPanel();
        }
    }
}
