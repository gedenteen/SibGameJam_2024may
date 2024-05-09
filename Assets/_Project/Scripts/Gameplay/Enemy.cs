using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float hitpoints = 2;

    private bool facingRight = true;

    private MainCharacter mainCharacter;

    private void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    private void FixedUpdate()
    {
        Move();
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

    private void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 eulerAngles = transform.eulerAngles;
        if (facingRight)
        {
            eulerAngles.y = 0f;
        }
        else
        {
            eulerAngles.y = 180f;
        }
        transform.eulerAngles = eulerAngles;
    }
}
