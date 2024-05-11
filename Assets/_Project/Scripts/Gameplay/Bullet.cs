using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform circleOrig;

    [Range(1, 10)]
    [SerializeField]
    private float speed = 10f;
    
    [Range(1, 10)]
    [SerializeField]
    private float lifeTime = 10f;

    private Rigidbody2D rb;
    public float radius;
    private MainCharacter mainCharacter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        //rb.velocity = transform.up * speed;
        mainCharacter = FindAnyObjectByType<MainCharacter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ReferenceEquals(collision.gameObject, mainCharacter.gameObject))
        {
            Health health;
            if (health = collision.GetComponent<Health>())
            {
                health.GetHit(1, mainCharacter.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
