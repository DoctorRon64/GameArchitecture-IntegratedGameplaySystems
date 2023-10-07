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

    public Enemy GetEnemy(GameObject _instance)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].Instance == _instance)
            {
                return enemies[i];
            }
        }

        Debug.LogError("EnemyManager Couldn't find Enemy");
        return null;
    }

    public void AddEnemy(string _enemyType, Vector2 _pos)
    {
        Enemy newEnemy = enemyFactory.Create(_enemyType);
        newEnemy.Instance.transform.position = new Vector3(_pos.x, -_pos.y, 0);
        enemies.Add(newEnemy);
        newEnemy.OnDied += RemoveEnemy;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        enemy.OnDied -= RemoveEnemy;
        GameObject.Destroy(enemy.Instance);
        enemy = null;
    }
}
