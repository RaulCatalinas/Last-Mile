using System;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float speed = 3f;
    [SerializeField] Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.linearVelocityX = -speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoadEnd"))
        {
            Destroy(gameObject);
        }
    }
}
