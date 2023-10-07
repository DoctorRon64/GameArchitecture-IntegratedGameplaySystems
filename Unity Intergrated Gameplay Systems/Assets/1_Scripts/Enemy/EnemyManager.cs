using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : IFixedUpdateable
{
    private EnemyFactory enemyFactory;
    private List<Enemy> enemies;

    public EnemyManager(GameSettings _gameSettings)
    {
        enemyFactory = new EnemyFactory(_gameSettings);
        enemies = new List<Enemy>();
    }

    public void OnFixedUpdate()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.OnFixedUpdate();
        }
    }

    public void AddEnemy(string _enemyType, Vector2 _pos)
    {
        Enemy newEnemy = enemyFactory.Create(_enemyType);
        newEnemy.Instance.transform.position = new Vector3(_pos.x, -_pos.y, 0);
        enemies.Add(newEnemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].Instance == enemy)
            {
                enemies.Remove(enemies[i]);
                break;
            }
        }

        Debug.LogError("EnemyManager can't find " + enemy.name + " in enemies");
    }
}
