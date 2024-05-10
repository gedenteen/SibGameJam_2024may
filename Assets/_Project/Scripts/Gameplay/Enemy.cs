using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float hitpoints = 2;
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
        transform.position = Vector2.MoveTowards(transform.position, mainCharacter.transform.position, speed * Time.fixedDeltaTime);

    }

    private void Attack()
    {
        Debug.Log("Attack");
    }
}
