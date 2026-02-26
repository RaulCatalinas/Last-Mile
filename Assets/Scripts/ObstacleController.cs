using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private AudioSource crashAudioSource;
    private ParticleSystem smokeParticleSystem;

    void Awake()
    {
        speed = ObstacleSpawner.Instance.GetRandomSpeed();
        rb = GetComponent<Rigidbody2D>();
        crashAudioSource = GetComponent<AudioSource>();
        smokeParticleSystem = GetComponent<ParticleSystem>();
    }
    void Start()
    {
        smokeParticleSystem.Stop();
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
        if (collision.collider.CompareTag("Player"))
        {
            crashAudioSource.Play();
            smokeParticleSystem.Play();
            GameManager.Instance.GameOver();
        }
    }
}
