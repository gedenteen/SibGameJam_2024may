using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Character
{
    // public float speed = 3f;
    // public float hitpoints = 2;
    public bool isDead = false;

    private MainCharacter mainCharacter;

    private void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    private void FixedUpdate()
    {
        Move();
        float distance = Vector2.Distance(transform.position, mainCharacter.transform.position);
        if (distance < 0.8f)
            Attack();
    }

    private void Move()
    {
        Vector3 oldPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, mainCharacter.transform.position, speed * Time.fixedDeltaTime);
        Vector3 offset = transform.position - oldPosition;

        if (!facingRight && offset.x > 0)
        {
            Flip();
        }
        else if (facingRight && offset.x < 0)
        {
            Flip();
        }

    }

    private void Attack()
    {
        Debug.Log("Attack");
    }
}
