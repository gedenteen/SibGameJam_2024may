using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter Instance { get; private set;} = null;
    
    private List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;    
    }

    public void AddEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            enemies.Add(enemy);
            Debug.Log($"EnemyCounter: AddEnemy: success, count={enemies.Count}");
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemy != null && enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Debug.Log($"EnemyCounter: RemoveEnemy: success, count={enemies.Count}");
        }
    }

    public int GetCountOfEnemies()
    {
        return enemies.Count;
    }
}
