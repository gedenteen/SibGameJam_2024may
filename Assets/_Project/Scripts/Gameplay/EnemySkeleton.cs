using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    [SerializeField] private float distanceForShoot = 8f;

    private void Awake()
    {
        // Debug.Log($"Enemy: Awake: EnemyCounter.Instance={EnemyCounter.Instance}");
        if (EnemyCounter.Instance != null)
        {
            EnemyCounter.Instance.AddEnemy(this);
        }
    }

    private void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    private void FixedUpdate()
    {
        Move();

        float distance = Vector2.Distance(transform.position, mainCharacter.transform.position);
        // Debug.Log($"Enemy: FixedUpdate: distance={distance}");
        if (distance <= distanceForShoot)
            Shoot();
    }

    private void Shoot()
    {
        Debug.Log($"EnemySkeleton: Shoot");
    }
}
