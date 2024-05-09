using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemy;
    [SerializeField] private float timeForSpawn = 3f;
    [SerializeField] private bool isDigging = false;

    private float timer = 3f;

    private void Awake()
    {
        timer = timeForSpawn / 2f;
    }

    private void FixedUpdate()
    {
        if (!isDigging)
        {
            return;
        }

        timer += Time.fixedDeltaTime;
        if (timer >= timeForSpawn)
        {
            Debug.Log($"ZombieSpawner: FixedUpdate: создаю зомбя");
            GameObject go = Instantiate(prefabEnemy, transform);
            timer = 0f;
        }
    }

    public void SetStateIsDigging(bool value)
    {
        //Debug.Log($"ZombieSpawner: SetStateIsDigging: begin");
        isDigging = value;
    }
}
