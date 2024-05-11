using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField]
    private float speed = 10f;
    
    [Range(1, 10)]
    [SerializeField]
    private float lifeTime = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        Health health;
        if (health = collision.contacts[0].collider.GetComponent<Health>())
        {
            health.GetHit(1, gameObject);
        }
        Destroy(gameObject);

    }
}
