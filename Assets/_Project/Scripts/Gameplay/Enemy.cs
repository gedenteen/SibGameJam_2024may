using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

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
        transform.position = Vector2.MoveTowards(transform.position, mainCharacter.transform.position, speed * Time.fixedDeltaTime);
    }
}
